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
        <Word v-for="(word, i) in WordsForCurrentPage" :key="i" :word.sync="word" :small="true"/>
        <pagination :pagination-info.sync="pages"/>
    </div>
</template>

<script>
    import searchField from "../common/SearchField";
    import Word from '../common/Word'
    import pagination from '../common/Pagination'

    const WORDS_PER_PAGE = 5;
    export default {
        name: "myWordsWindow",
        components: {searchField, Word, pagination},
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
                pages: {
                    page: 1,
                    totalPages: 1
                },
                words: []
            }
        },
        beforeCreate () {
            this.$store.dispatch('getMyWords')
                .then(words => {
                    this.words = words;
                    this.pages.totalPages = Math.ceil(words.length / WORDS_PER_PAGE);
                });
        },
        computed: {
            WordsForCurrentPage() {
                const start = (this.pages.page - 1) * WORDS_PER_PAGE;
                const end = start + WORDS_PER_PAGE;
                const words = this.words.slice(start, end);

                return words;
            }
        }
    };
</script>

<style lang="scss" scoped>
    .form-control:focus {
        outline: none;
    }
</style>