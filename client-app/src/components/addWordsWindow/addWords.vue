<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
    <form @submit.prevent="handleSubmit(submitWords)">
        <p class="display-4">Add new words!</p>
        <p class="display-4">
            Found {{foundWords.length}} new word{{foundWords.length > 1 ? "s" : ''}}
            in Text: "{{textName}}"
        </p>
        <p class="alert alert-secondary">
            If you don't want to repeat a word, just leave the translation field empty
        </p>
        
        <div v-for="word in words" 
            :key="word.Content">
            
            <word-to-add :word.sync="word"></word-to-add>
        </div>
        
        <button type="submit" class="btn mr-2 btn-primary" :disabled="failed">
            Submit
        </button>
        <button class="btn btn-secondary" @click.prevent="discardWords">
            Cancel
        </button>
    </form>
    </ValidationObserver>
</template>

<script>
    import wordToAdd from '../common/WordToAdd'
    import InputField from '@/components/common/InputField'
    import business from '../common/business/addWords'
    import {mapGetters} from 'vuex'
    import Vue from 'vue'
    import { ValidationObserver } from 'vee-validate'
    import ValidationProvider from '../common/validation'
    
    export default {
        name: "addTranslation",
        components: {ValidationObserver, wordToAdd},
        data() {
            return {
                words: []
            }
        },
        methods: {
            submitWords() {
                let words = this.words;
                const csvLift = business.CSVtoArray;
                words = words.map(w => {
                    w.translations = csvLift(w.translationsField).map(t => ({content: t}));
                    w.tags = csvLift(w.tagsField).map(t => ({name: t}));
                    w.familiar = !w.translationsField || w.familiar;
                    return w;
                });
                
                business.submitWords(words)
                    .then(words => this.$store.dispatch('updateUserInfo', words))
                    .then(_ =>  this.$router.push({name: 'myWords'}));
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
            ]),
        },
        beforeMount() {
            const words = this.$store.getters.foundWords;
            console.log(words);
            this.words = [...words]
                .map(w => {w.tagsField = w.translationsField = ''; return w});
        }
    }
</script>

<style lang="scss" scoped>
.word {
    padding: 1em;
    margin: 1em 0;
    position: relative;
    border-radius: .5rem;

    &_content_display {
        line-height: 1.2;
        border: 2px solid transparent;
        font-size: 2rem;
        margin-bottom: .5rem;
    }

    &_familiar {
        background-color: #0062cc;
        color: ghostwhite;
        font-weight: bolder;
    }
}
</style>