<template>
    <div class="input_wrapper">
        <div class="token__container">
            <span v-for="token in selected" :key="token.id" 
                class="badge badge-pill badge-secondary token">
                {{token.name || token.content}}
                <button class="delete-token" @mouseup="removeToken(token)">x</button>
            </span>
        </div>
        <input class="form-control input_field" type="search"
               :placeholder="placeholder" aria-label="Search"
               v-model="inputString"
               ref="input"
               @focus="focus"
               @blur="search"
               @keyup.enter="search"
               @input="checkMatches"
               @keyup.delete="checkMatches"/>
        <ul :class="{input_matches: true, input_show: matches.length > 0}">
            <li class="input_match" v-for="(match, i) in matches"
                :key="i"
                @click="addToken(match)">{{match}}
            </li>
        </ul>
        <small class="info form-text error" v-if="error.length">{{error}}</small>
    </div>
</template>

<script>
    import business from "../common/business/searchField"

    export default {
        name: "SearchField",
        props: ["config"],
        data() {
            return {
                placeholder: this.config.placeholder || "",
                inputString: "",
                error: "",
                matches: [],
                selected: [],
                searchTimer: null
            }
        },
        methods: {
            checkMatches() {
                let tokens = this.config.tokens.map(t => t.name || t.content);
                let selected = this.selected.map(s => s.name || s.content);
                this.matches = business.checkMatches(this.inputString, tokens)
                        .filter(match => !~selected.indexOf(match));
            },
            addToken(value) {
                this.inputString = '';
                let match = this.config.tokens
                    .find(t => t.name == value || t.content == value);
                this.selected.push(match);
            },
            removeToken(value) {
                this.selected.splice(this.selected.indexOf(value), 1);
            },
            search(e) { 
                e.preventDefault();
                
                setTimeout(() => {
                    this.inputString = '';
                    this.matches = [];
                }, 90);
                this.$emit('finished-typing'); 
            },
            focus() { 
                this.checkMatches();
                this.$emit('focus');
            }
        }
    }
</script>

<style lang="scss" scoped>
    .token__container{
        font-size: 1.05rem;
        padding-bottom: 3px;
    }
    .token {
        margin-right: .2em;
    }
    .delete-token {
        color: #343434;
        font-family: monospace;
        border-radius: 50%;
        height: 1em;
        width: 1em;
        box-shadow: none;
        padding: 0;
        border: none;
        background-color: rgb(200, 200, 200);
    }
    .input_field:focus {
        border: none;
    }
    .input_ {
        &wrapper {
            position: relative;
            padding: 0;
        }

        &field {
            width: 100%;
        }

        &matches {
            margin: 0;
            padding: 0;
            list-style-type: none;
            display: none;
            position: absolute;
            left: 0;
            right: 0;
            height: 11.25em;
            overflow-y: auto;
            bottom: -11.5em;
            z-index: 1;
        }

        &show {
            display: block;
        }

        &match {
            display: block;
            text-align: center;
            font-size: 1rem;
            padding: .375em .75em;
            background-color: #f8f9fa;
            border-radius: .2em;
            transition: .3s;

            &:hover {
                background-color: #0062cc;
                color: white;
                cursor: pointer;
                transition: .15s;
            }
        }
    }
</style>