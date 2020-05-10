<template>
    <form>
        <p class="display-4">Add new words!</p>
        <p class="display-4">Found {{foundWords.length}} words in Text: "{{textName}}"</p>
        <p>If you don't want to repeat a word, just leave the translation field empty.</p>
        
        <pageOfWords :words="foundWords" :fresh="true"
            @discard="discardWords"
            @submit="submitWords"/>
    </form>
</template>

<script>
    import word from '../common/Word'
    import business from '../common/business/addWords'
    import pageOfWords from '@/components/pageOfWords/pageOfWords'
    import {mapGetters} from 'vuex'
    import Vue from 'vue'
    export default {
        name: "addTranslation",
        components: {pageOfWords},
        methods: {
            submitWords(words) {
                business.submitWords(words)
                    .then(() => {
                        if(!this.foundWords.length)
                            this.$router.push({name: 'myWords'}) 
                    });
            },
            discardWords() {
                business.discardWords()
                    .then(_ => {
                        this.words = [];
                        this.$router.push({name: "supplyText"})
                    });
            }
        },
        computed: {
            ...mapGetters([
                'foundWords',
                'textName'
            ])
        }
    }
</script>

<style>

</style>