<template>
    <div class="input_wrapper">
        <input class="form-control input_field" type="search"
               :placeholder="placeholder" aria-label="Search"
               v-model="inputString"
               ref="input"
               @blur="beginSearch"
               @input="checkMatches"
               @keyup.delete="checkMatches"/>
        <ul :class="{input_matches: true, input_show: matches.length > 0}">
            <li class="input_match" v-for="(match, i) in matches"
                :key="i"
                @click="setMatchToInput(match)">{{match}}
            </li>
        </ul>
        <small class="info form-text error" v-if="error.length">{{error}}</small>
        <small class="info form-text" v-else>
            {{CSV ? "Use Comma to separate values" : ''}}</small>
    </div>
</template>

<script>
    export default {
        name: "SearchField",
        props: ["config"],
        data() {
            return {
                placeholder: this.config ? this.config.placeholder : "",
                CSV: this.config.CSV,
                inputString: "",
                error: "",
                matches: [],
            }
        },
        methods: {
            checkMatches() {
                if (this.invalidString() || !this.inputString.length)
                    return this.matches = [];

                const value = this.CSV ? this.inputString.split(',').pop() : this.inputString;

                if (value)
                    this.matches = this.config.tokens.filter(t => {
                        const matchesToken = ~t.indexOf(value.trim());
                        const exists = ~this.inputString.indexOf(t + ',');
                        return matchesToken && !exists;
                    });
                else {
                    this.matches = this.config.tokens
                        .filter(t => !~this.inputString.indexOf(t + ','));
                }
            },
            setMatchToInput(match) {
                if (~this.inputString.indexOf(',') && this.CSV) {
                    this.inputString = this.inputString.split(',').slice(0, -1).join(',') + ',';
                    this.inputString += match;
                } else {
                    this.inputString = match;
                }

                this.$nextTick(() => this.$refs.input.focus());

                this.matches = [];
            },
            invalidString() {
                if (this.inputString && !(/^[\w\s]+(,[\w\s]*)*$/).test(this.inputString)) {
                    return this.error = "Remove invalid characters";
                }
                return this.error = "";
            },
            beginSearch() {
                this.$emit('finished-typing');
            }
        }
    }
</script>

<style lang="scss" scoped>
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
            top: 2.5em;
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