<template>
    <div class="container-fluid border word" :class="{'word_familiar':word.familiar && !word.editing}">
        
        <validation-observer v-if="word.editing">
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
                        <input type="checkbox" class="custom-control-input" id="dont_repeat"
                               v-model="word.familiar">
                        <label class="custom-control-label" for="dont_repeat">Don't repeat!</label>
                    </div>
                </div>
            </div>
            
            <button type="button" class="btn btn-sm btn-secondary edit_button edit_button-discard"
                    @click="discardChanges">Discard changes
            </button>
            <button type="button" class="btn btn-sm btn-primary edit_button edit_button-save"
                    @click="saveChanges">Save changes
            </button>
        </validation-observer>

        <template v-else>
            <div class="row justify-content-center">
                <div class="col-8">
                    <p class="display-4 text-center word_content_display"
                        :class="{'word_content_display-small': small}"
                        @mouseenter="(() => small ? showTranslation : () => {})()"
                        @mouseleave="(() => small ? showContent : () => {})()">
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
        props: ['word', 'small'],
        components: {InputField, ValidationObserver},
        data() {
            return {
                contentField: {
                    id: `word${this.word.id}_content-input`,
                    type: 'text',
                    smallText: 'word to learn',
                    name: 'Word',
                    rules: 'alpha|required|min:3',
                    placeholder: 'Input the word, please',
                    value: this.word.content
                },
                tagField: {
                    id: `tags${this.word.id}_input`,
                    type: 'text',
                    smallText: '',
                    name: 'Tags',
                    rules: 'alpha|required',
                    placeholder: 'Input tags, please',
                    value: this.word.tags.join(', ')
                },
                translationField: {
                    id: `translations${this.word.id}_input`,
                    type: 'text',
                    smallText: 'translations of the word',
                    name: 'Translations',
                    rules: 'alpha|required',
                    placeholder: 'Input tags, please',
                    value: this.word.tags.join(', ')
                },
                content: this.word.content,
                animation: null
            }
        },
        methods: {
            beginEdition(e) {
                this.word.editing = true;

                this.tagField.value = this.word.tags.join(', ');
                this.translationField.value = this.word.translations.join(', ');
            },
            saveChanges(e) {
                this.word.content = this.content = this.contentField.value;
                this.word.tags = this.parseCsvIntoArray(this.tagField.value);
                this.word.translations = this.parseCsvIntoArray(this.translationField.value);

                this.word.editing = false;
            },
            discardChanges(e) {
                this.tagField.value = this.word.tags.join(', ');
                this.translationField.value = this.word.translations.join(', ');

                this.word.editing = false;
            },
            showTranslation() {
                clearInterval(this.animation);
                const translation = this.word.translations[0];
                this.animation = 
                    setInterval(this.changeStringStepByStep, 30, translation);
            },
            showContent() {
                clearInterval(this.animation);
                const content = this.word.content;
                this.animation =
                    setInterval(this.changeStringStepByStep, 30, content);
            },
            changeStringStepByStep(to) {
                let from = this.content;

                if(from == to && !to) {
                    clearInterval(this.animation);
                    return;
                }

                if(to.indexOf(from) != 0){
                    from = from.length > 1 ?
                        from.substring(0, from.length-1) :
                        to[0];
                    this.content = from;
                }
                else {
                    from += from.length < to.length ?
                        to[from.length] :
                        '';
                    this.content = from;
                }
            },
            parseCsvIntoArray(csv) {
                return csv
                    .split(',')
                    .filter(t => t.trim())
                    .map(t => t.trim());
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