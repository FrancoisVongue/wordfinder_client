<template>
    <div class="window">
        <p class="display-4">My words</p>
        <div class="">
            <form class="row form-inline align-items-end">
                <div class="col-md-4">
                    <searchField
                            @focus="stopSearch"
                            @finished-typing="searchWords"
                            :config="searchConfigs.word"/>
                </div>
                
                <div class="col-md-4">
                    <searchField
                            @focus="stopSearch"
                            @finished-typing="searchWords"
                            :config="searchConfigs.tags"/>
                </div>
                
                <div class="col-md-4">
                    <searchField
                            @focus="stopSearch"
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
        <template v-else-if="wordsList.length > 0">
            <pageOfWords :words = "wordsList" :pageNumber.sync = "page"/>
        </template>
        <template v-else>
            <p class="display-4">Not a single word was found :(</p>
        </template>
    </div>
</template>

<script>
    import {mapGetters} from 'vuex'
    import searchField from "../common/SearchField";
    import pageOfWords from '@/components/pageOfWords/pageOfWords'

    export default {
        name: "myWordsWindow",
        components: {searchField, pageOfWords},
        data() {
            return {
                searchConfigs: {
                    word: {
                        placeholder: "input word",
                        tokens: [],
                        content: '',
                    },
                    tags: {
                        placeholder: "input tags",
                        tokens: [],
                        chosenTokens: [],
                    },
                    topics: {
                        placeholder: "input topics",
                        tokens: [],
                        chosenTokens: [],
                    }
                },
                page: 1,
                wordsList: [],
                loading: true,
                requestSendLatency: null
            }
        },
        methods: {
            searchWords() {
                this.requestSendLatency = setTimeout(() => {
                    this.loading = true;
                    this.$store.dispatch('getMyWords')
                        .then(this.updateWordList);
                }, 300)
            },
            updateWordList(words) {
                this.wordsList = words;
                this.page = 1;
                this.loading = false;
            },
            setTokens(user) {
                this.searchConfigs.word.tokens = user.words.map(w => w.content);
                this.searchConfigs.tags.tokens = user.tags.map(t => t.name);
                this.searchConfigs.topics.tokens = user.topics.map(t => t.name);
                console.log(this.searchConfigs)
            },
            stopSearch() {
                clearTimeout(this.requestSendLatency);
            }
        },
        mounted () {
            this.$store.dispatch('getMyWords')
                .then(this.updateWordList);
                
            this.setTokens(this.userInfo);
        },
        computed: {
            ...mapGetters(['userInfo'])
        }
    };
</script>

<style lang="scss" scoped>
    
</style>