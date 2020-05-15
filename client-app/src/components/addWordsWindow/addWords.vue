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
        
            <!-- class="form-group row justify-content-center" -->
        <div v-for="word in words" 
            class="container-fluid border word form-group row justify-content-center"
            :key="word.Content">
            
            <div class="col-sm-4">
                <ValidationProvider name="Content field" 
                    rules="alpha_dash|required|min:3" 
                    v-slot="{errors}">
                    <label :for="word.Content + 'content'">Word</label>
                    <input 
                        type="text" class="form-control" 
                        :id="word.Content + 'content'" 
                        placeholder="word"
                        v-model="word.Content">
                    <small :class="{error: errors.length > 0}" 
                        class="info form-text validation_info">
                        {{errors[0]}}
                    </small>
                </ValidationProvider>
            </div>
        
            <div class="col-sm-4">
                <ValidationProvider name="Tags field" 
                    rules="CSV" 
                    v-slot="{errors}">
                    
                    <label :for="word.Content + 'tags'">Tags</label>
                    <input 
                        type="text" class="form-control" 
                        :id="word.Content + 'tags'" 
                        placeholder="tags"
                        v-model="word.Tags">
                    <small :class="{error: errors.length > 0}" 
                        class="info form-text validation_info">
                        {{errors[0] || "Tags to assign"}}
                    </small>
                </ValidationProvider>
            </div>
            
            <div class="col-sm-4">
                <ValidationProvider name="Translations field" 
                    rules="CSV" 
                    v-slot="{errors}">
                    
                    <label :for="word.Content + 'translations'">Translations</label>
                    <input 
                        type="text" class="form-control" 
                        :id="word.Content + 'translations'" 
                        placeholder="translations"
                        v-model="word.Tags">
                    <small :class="{error: errors.length > 0}" 
                        class="info form-text validation_info">
                        {{errors[0] || "Translations"}}
                    </small>
                </ValidationProvider>
            </div>

            <div class="col-sm-12 text-center">
                <div class="custom-control custom-checkbox">
                    <input type="checkbox" class="custom-control-input" 
                        :id="'dont_repeat' + word.Content"
                        v-model="word.Familiar">
                    <label class="custom-control-label" for="dont_repeat">
                        Don't repeat!
                    </label>
                </div>
            </div>
        </div>
        
    </form>
    </ValidationObserver>
</template>

<script>
    import word from '../common/Word'
    import InputField from '@/components/common/InputField'
    import business from '../common/business/addWords'
    import {mapGetters} from 'vuex'
    import Vue from 'vue'
    import { ValidationObserver } from 'vee-validate'
    import ValidationProvider from '../common/validation'
    
    export default {
        name: "addTranslation",
        components: {ValidationObserver, ValidationProvider},
        data() {
            return {
                words: []
            }
        },
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
            ]),
        },
        beforeMount() {
            const words = this.$store.getters.foundWords;
            console.log(words);
            this.words = [...words];
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