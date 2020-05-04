<template>
<ValidationObserver v-slot="{ failed, handleSubmit }">
    <form>
        <p class="display-4">Add new words!</p>
        <p>
            If you don't want to repeat a word, just leave the translation field empty.
        </p>
        <p class="display-4">Found {{foundWords.length}} words in Text: "{{textName}}"</p>
        
        <pageOfWords :words="words" :pageNumber.sync="page" :fresh="true"
            @submit.prevent="handleSubmit(submitWords)"
            @discard = "discardWords"/>
    </form>
</ValidationObserver>
</template>

<script>
    import word from '../common/Word'
    import pageOfWords from '@/components/pageOfWords/pageOfWords'
    import {ValidationObserver} from 'vee-validate'
    import {mapGetters} from 'vuex'
    import Vue from 'vue'

    export default {
        name: "addTranslation",
        components: {ValidationObserver, pageOfWords},
        data() {
            return {
                page: 1,
                words: []
            }
        },
        created() {
            this.words = [...this.foundWords];  
        },
        methods: {
            submitWords(words) {
                this.$store.dispatch('submitWords', this.words)
                    .then(() => {
                        if(!this.foundWords.length)
                            this.$router.push({name: 'myWords'})
                    });
            },
            discardWords() {
                this.words = [];
                this.$store.dispatch('discardWords')
                    .then(_ => 
                        Vue.nextTick()
                            .then(_ => this.$router.push({name: "supplyText"})));
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