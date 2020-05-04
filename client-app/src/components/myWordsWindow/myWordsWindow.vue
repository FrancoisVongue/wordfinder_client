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
        <template v-else>
            <pageOfWords :words = "wordsList" :pageNumber.sync = "page"/>
        </template>
    </div>
</template>

<script>
    import {mapGetters} from 'vuex'
    import searchField from "../common/SearchField";
    import pageOfWords from '@/components/pageOfWords/pageOfWords'
    import searchConfigs from './searchConfigs'

    export default {
        name: "myWordsWindow",
        components: {searchField, pageOfWords},
        data() {
            return {
                searchConfigs: searchConfigs,
                page: 1,
                wordsList: [],
                loading: true
            }
        },
        methods: {
            searchWords() {
                this.loading = true;
                this.$store.dispatch('getMyWords')
                    .then(this.updateWordList);
            },
            updateWordList(words) {
                this.wordsList = words;
                this.page = 1;
                this.loading = false;
            }
        },
        mounted () {
            this.$store.dispatch('getMyWords')
                .then(this.updateWordList);
        }
    };
</script>

<style lang="scss" scoped>
    
</style>