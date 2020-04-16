<template>
    <div class="window">
        <p class="display-4">My words</p>
        <div class="">
            <form class="row form-inline align-items-start">
                <div class="col-md-4">
                    <searchField
                            @finished-typing="searchWords"
                            :config="searchConfigs.word"/>
                </div>
                
                <div class="col-md-4">
                    <searchField
                            @finished-typing="searchWords"
                            :config="searchConfigs.tags"/>
                </div>
                
                <div class="col-md-4">
                    <searchField
                            @finished-typing="searchWords"
                            :config="searchConfigs.topics"/>
                </div>
            </form>
        </div>

        <template v-if="loading">
            <div class="d-flex justify-content-center auth_spinner align-items-center">
                <div class="spinner-grow" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
        </template>
        <template v-else-if="WordsForCurrentPage.length">
            <Word v-for="word in WordsForCurrentPage" :key="word.id" :word.sync="word"/>
            <pagination :pagination-info.sync="pagination"/>
        </template>
    </div>
</template>

<script>
    import {mapGetters} from 'vuex'
    import searchField from "../common/SearchField";
    import Word from '../common/Word'
    import pagination from '../common/Pagination'

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
                pagination: {
                    page: 1,
                    perPage: 5,
                    items: []
                },
                loading: true
            }
        },
        methods: {
            searchWords() {
                this.loading = true;
                this.$store.dispatch('getMyWords')
                    .then(words => {
                        this.pagination.items = words;
                        this.loading = false;
                    });
                // todo: update the page
            }
        },
        mounted () {
            this.$store.dispatch('getWordsInfo')
                .then(info => {
                    this.searchConfigs.word.tokens = info.words;
                    this.searchConfigs.tags.tokens = info.tags;
                    this.searchConfigs.topics.tokens = info.topics;
                });
            this.$store.dispatch('getMyWords')
                .then(words => {
                    this.pagination.items = words;

                    this.loading = false;
                });
        },
        computed: {
            WordsForCurrentPage() {
                const start = (this.pagination.page - 1) * this.pagination.perPage;
                const end = start + this.pagination.perPage;
                const words = this.pagination.items.slice(start, end);

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