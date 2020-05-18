<template>
    <div class="container-fluid border word form-group row justify-content-center">
        <div class="col-sm-4">
            <ValidationProvider :name="'Content field of ' + word.content" 
                rules="alpha_dash|required|min:3" 
                v-slot="{errors}">
                <label :for="word.content + 'content'">Word</label>
                <input 
                    type="text" class="form-control" 
                    :id="word.content + 'content'" 
                    placeholder="word"
                    v-model="word.content">
                <small :class="{error: errors.length > 0}" 
                    class="info form-text validation_info">
                    {{errors[0]}}
                </small>
            </ValidationProvider>
        </div>
    
        <div class="col-sm-4">
            <ValidationProvider :name="'Tags field of ' + word.content"
                rules="CSV" 
                v-slot="{errors}">
                
                <label :for="word.content + 'tags'">Tags</label>
                <input 
                    type="text" class="form-control" 
                    :id="word.content + 'tags'" 
                    placeholder="tags"
                    v-model="word.tagsField">
                <small :class="{error: errors.length > 0}" 
                    class="info form-text validation_info">
                    {{errors[0] || "Tags to assign"}}
                </small>
            </ValidationProvider>
        </div>
        
        <div class="col-sm-4">
            <ValidationProvider :name="'Translations field of ' + word.content" 
                rules="CSV" 
                v-slot="{errors}">
                
                <label :for="word.content + 'translations'">Translations</label>
                <input 
                    type="text" class="form-control" 
                    :id="word.content + 'translations'" 
                    placeholder="translations"
                    v-model="word.translationsField">
                <small :class="{error: errors.length > 0}" 
                    class="info form-text validation_info">
                    {{errors[0] || "Translations"}}
                </small>
            </ValidationProvider>
        </div>

        <div class="col-sm-12 text-center">
            <div class="custom-control custom-checkbox">
                <input type="checkbox" class="custom-control-input" 
                    :id="'dont_repeat' + word.content"
                    v-model="word.familiar">
                <label class="custom-control-label" :for="'dont_repeat' + word.content">
                    Don't repeat!
                </label>
            </div>
        </div>
    </div>
</template>

<script>
    import fieldConfig from '../common/fieldConfig/wordToAdd'
    import ValidationProvider from '../common/validation'
    
    export default {
        name: 'Word',
        props: ['word'],
        components: {ValidationProvider},
        data() {
            return {
                contentField: fieldConfig.contentField(this.word),
                tagField: fieldConfig.tagField(this.word),
                translationField: fieldConfig.translationField(this.word),
                editing: this.fresh,
                familiar: this.word.familiar
            }
        },
        methods: {
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