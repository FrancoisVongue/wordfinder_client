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
            
            <template v-if="!fresh">
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
            </template>
        </ValidationObserver>

        <template v-else>
            <div class="row justify-content-center">
                <div class="col-8">
                    <p class="display-4 text-center word_content_display">
                        {{content}}
                    </p>
                </div>
            </div>
            
            <button type="button" 
                class="btn btn-sm btn-warning edit_button edit_button-change"
                @click="beginEdition">
                Edit word
            </button>
        </template>
    </div>
</template>

<script>
    import InputField from '../common/InputField'
    import fieldConfig from '../common/fieldConfig/word'
    import business from '../common/business/word'
    import { ValidationObserver } from 'vee-validate'
    
    export default {
        name: 'Word',
        props: ['word', 'fresh'],
        components: {InputField, ValidationObserver},
        data() {
            return {
                contentField: fieldConfig.contentField(this.word),
                tagField: fieldConfig.tagField(this.word),
                translationField: fieldConfig.translationField(this.word),
                editing: this.fresh,
                familiar: this.word.familiar
            }
        },
        created() {
            this.setFieldValues();  
        },
        computed: {
            content() { return this.word.content },
            translations() { return this.word.translations },
            tags() { return this.word.tags }
        },
        methods: {
            beginEdition(e) {
                this.editing = true;
                this.setFieldValues();
            },
            saveChanges(e) {
                this.word.content = this.contentField.value;
                this.word.tags = 
                    business.CSVtoArrayOfObjects(this.tagField.value, "name");
                this.word.translations = 
                    business.CSVtoArrayOfObjects(this.translationField.value, "content");
                this.word.familiar = this.familiar;

                this.editing = false;
            },
            discardChanges(e) { 
                this.editing = false;
            },
            setFieldValues() {
                ({  content: this.contentField.value,
                    tags: this.tagField.value,
                    translations: this.translationField.value} = 
                    
                    business.setFieldValues(this.word));
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