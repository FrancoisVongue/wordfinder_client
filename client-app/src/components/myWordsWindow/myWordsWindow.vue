<template>
    <div class="window">
        <p class="display-4">My Words</p>
        <div class="">
            <form class="row form-inline align-items-start">
                <div class="col-md-4">
                    <searchField :config="searchConfigs.word"/>
                </div>
                
                <div class="col-md-4">
                    <searchField :config="searchConfigs.tags"/>
                </div>
                
                <div class="col-md-4">
                    <searchField :config="searchConfigs.topics"/>
                </div>
            </form>
        </div>
        <Word v-for="(word, i) in words" :key="i" :word.sync="word" :small="true"/>
    </div>
</template>

<script>
    import searchField from "../common/SearchField";
    import Word from '../common/Word'

    export default {
        name: "myWordsWindow",
        components: {searchField, Word},
        data() {
            return {
                searchConfigs: {
                    word: {
                        placeholder: "input word",
                        tokens: ["hello", "world", "nice", "extracurricular"],
                        CSV: false
                    },
                    tags: {
                        placeholder: "input tags",
                        tokens: ["greeting", "home", "work"],
                        CSV: true
                    },
                    topics: {
                        placeholder: "input topics",
                        tokens: ["hellsing", "matrix", "mr.nobody"],
                        CSV: true
                    },
                },
                words: [],
                page: 0
            }
        },
        beforeMount () {
            this.$store.dispatch('getMyWords')
                .then(words => this.words = words);
        }
    };
</script>

<style lang="scss" scoped>
    .form-control:focus {
        outline: none;
    }
</style>