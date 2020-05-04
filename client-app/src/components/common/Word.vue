<template>
    <div class="container-fluid border word"
         :class="{'word_familiar':word.familiar && !editing}">
        
        <ValidationObserver v-if="editing" v-slot="{ failed, handleSubmit }">
            <div class="form-group row justify-content-center">
            
                <div class="col-sm-4">
                    <input-field :field-data.sync="contentField"/>
                </div>
            
                <div class="col-sm-4">
                    <input-field :field-data.sync="tagField"/>
                </div>
                
                <div class="col-sm-4">
                    <input-field :field-data.sync="translationField"/>
                </div>

                <div class="col-sm-12 text-center">
                    <div class="custom-control custom-checkbox">
                        <input type="checkbox" class="custom-control-input" 
                            id="dont_repeat"
                            v-model="familiar">
                        <label class="custom-control-label" for="dont_repeat">
                            Don't repeat!
                        </label>
                    </div>
                </div>
            </div>
            
            <button type="button" class="btn btn-sm btn-secondary
                edit_button edit_button-discard" 
                @click="discardChanges">
                Discard changes
            </button>
            <button type="button" class="btn btn-sm btn-primary
                edit_button edit_button-save"
                @click="handleSubmit(saveChanges)">
                Save changes
            </button>
        </ValidationObserver>

        <template v-else>
            <div class="row justify-content-center">
                <div class="col-8">
                    <p class="display-4 text-center word_content_display">
                        {{content}}
                    </p>
                </div>
            </div>
            
            <button type="button" class="btn btn-sm btn-warning edit_button edit_button-change"
                    @click="beginEdition">Edit word
            </button>
        </template>
    </div>
</template>

<script>
    import InputField from '../common/InputField'
    import { ValidationObserver } from 'vee-validate'
    
    export default {
        name: 'Word',
        props: ['word', 'fresh'],
        components: {InputField, ValidationObserver},
        data() {
            return {
                contentField: {
                    id: `word${this.word.id}_content-input`,
                    type: 'text',
                    smallText: '',
                    name: 'Word',
                    rules: 'alpha|required|min:3',
                    placeholder: 'Input the word, please',
                    value: ''
                },
                tagField: {
                    id: `tags${this.word.id}_input`,
                    type: 'text',
                    smallText: 'tags to assign',
                    name: 'Tags',
                    rules: '',
                    placeholder: 'Input tags, please',
                    value: ''
                },
                translationField: {
                    id: `translations${this.word.id}_input`,
                    type: 'text',
                    smallText: 'translations of the word',
                    name: 'Translations',
                    rules: '',
                    placeholder: 'Input translations, please',
                    value: ''
                },
                editing: false,
                familiar: this.word.familiar
            }
        },
        computed: {
            content() { return this.word.content },
            translations() { return this.word.translations },
            tags() { return this.word.tags }
        },
        methods: {
            beginEdition(e) {
                this.editing = true;
                
                this.contentField.value = this.word.content;
                this.tagField.value = this.word.tags
                    .map(t => t.name)
                    .join(', ');
                this.translationField.value = this.word.translations
                    .map(t => t.content)
                    .join(', ');
            },
            saveChanges(e) {
                this.word.content = this.contentField.value;
                this.word.tags = 
                    this.CSVtoArrayOfObjects(this.tagField.value, "name");
                this.word.translations = 
                    this.CSVtoArrayOfObjects(this.translationField.value, "content");
                this.word.familiar = this.familiar;

                this.editing = false;
            },
            discardChanges(e) { 
                this.editing = false;
            },
            CSVtoArrayOfObjects(csv, name) {
                const result = csv
                    .split(',')
                    .filter(t => t.trim())
                    .map(t => {return {[name]: t.trim()}});
                return result;
                
            }
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

    .edit_button {
        position: absolute;
        right: 0;
        
        &-change, 
        &-save {
            bottom: 0;
        }
        
        &-discard {
            top: 0;
        }
    }
</style>