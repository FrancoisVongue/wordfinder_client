<template>
    <div class="window">
        <p class="display-4">My words</p>
        <div class="">
            <form class="row form-inline align-items-end" 
                @submit.prevent="searchWords">
                
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
                        free_query: true
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
                    let tagIds = this.searchConfigs.tags.chosenTokens.map(t => t.id);
                    let topicIds = this.searchConfigs.topics.chosenTokens.map(t => t.id);
                    let wordContent = this.searchConfigs.word.content;
                    let searchConfig = {
                        Content: wordContent,
                        TagIds : tagIds,
                        TopicIds : topicIds
                    };
                    
                    if(this.empty(searchConfig))
                        this.$store.dispatch('getMyWords')
                            .then(this.updateWordList);
                    else 
                        this.$store.dispatch('searchMyWords', searchConfig)
                            .then(this.updateWordList);
                }, 300)
            },
            updateWordList(words) {
                this.wordsList = words;
                this.page = 1;
                this.loading = false;
            },
            setTokens(user) {
                this.searchConfigs.word.tokens = user.words;
                this.searchConfigs.tags.tokens = user.tags;
                this.searchConfigs.topics.tokens = user.topics;
            },
            stopSearch() {
                clearTimeout(this.requestSendLatency);
            },
            empty(o) {
                return [...Object.values(o)].every(v => !v || v.length == 0);
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