<template>
    <div class="container-fluid border word">
        
        <template v-if="word.editing">
            <div class="row justify-content-center">
                <div class="col-lg-8 text-center">
                    <p>
                        <input type="text" class="display-4 text-center word_content_input"
                            v-model="word.content">
                    </p>
                </div>
            </div>
            
            <div class="row row_description">
                <p class="text-center col-sm-4 col-form-label">Translation</p>
                <p class="text-center col-sm-4 col-form-label">Tags</p>
                <p class="text-center col-sm-4 col-form-label">Notes</p>
            </div>
            
            <div class="form-group row">
                <div class="col-sm-4">
                    <input type="text" class="form-control"
                           id="translation_input" placeholder="translation"
                           v-model="translationsString">
                </div>
                
                <div class="col-sm-4">
                    <input type="text" class="form-control"
                           id="tags_input" placeholder="tags"
                           v-model="tagsString">
                </div>
                
                <div class="col-sm-4">
                    <textarea class="form-control" id="notes_input"
                        placeholder="description" rows="1"
                        
                        v-model="word.notes">
                    </textarea>
                </div>
            </div>

            <div class="form-group row justify-content-center">
                <div class="col-sm-4 text-center">
                    <div class="custom-control custom-checkbox">
                        <input type="checkbox" class="custom-control-input" id="dont_repeat"
                               v-model="word.familiar">
                        <label class="custom-control-label" for="dont_repeat">Don't repeat!</label>
                    </div>
                </div>
            </div>
            <button type="button" class="btn btn-sm btn-danger edit_button edit_button-discard"
                    @click="discardChanges">Discard changes
            </button>
            <button type="button" class="btn btn-sm btn-success edit_button edit_button-save"
                    @click="saveChanges">Save changes
            </button>
        </template>

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
            
            <template v-if="!small">
                <div class="row row_description">
                    <p class="text-center col-sm-4 col-form-label">Translation</p>
                    <p class="text-center col-sm-4 col-form-label">Tags</p>
                    <p class="text-center col-sm-4 col-form-label">Notes</p>
                </div>
                
                <div class="form-group row">
                    <div class="col-sm-4">
                        <p class="form-control" id="translation">
                            <span v-if="word.translations.length == 0">No Translation</span>
                            <span v-for="(translation, i) in word.translations" :key="i">
                                <span v-if="i > 0">, </span>{{translation}}</span>
                        </p>
                    </div>
                    <div class="col-sm-4">
                        <p class="form-control" id="tags">
                            <span v-if="word.tags.length == 0">No Tags</span>
                            <span v-for="(tag, i) in word.tags" :key="i">
                                <span v-if="i > 0">, </span>{{tag}}</span>
                        </p>
                    </div>
                    <div class="col-sm-4">
                        <p class="form-control" id="notes">
                            <span v-if="word.notes.length == 0">No Description</span>
                            {{word.notes}}
                        </p>
                    </div>
                </div>

                <div class="form-group row justify-content-center">
                    <div class="col-sm-4 text-center">
                        <div class="custom-control custom-checkbox">
                            <input type="checkbox" class="custom-control-input" id="is_repeated" disabled
                                :checked="!word.familiar">
                            <label class="custom-control-label" for="is_repeated"><span
                                    v-if="word.familiar">Is not</span> Repeated!</label>
                        </div>
                    </div>
                </div>
            </template>
            <button type="button" class="btn btn-sm btn-warning edit_button edit_button-change"
                    @click="beginEdition">Edit word
            </button>
        </template>
    </div>
</template>

<script>
    export default {
        name: 'Word',
        props: ['word', 'small'],
        data() {
            return {
                tagsString: "",
                translationsString: "",
                content: this.word.content,
                animation: null
            }
        },
        methods: {
            autoGrow(e) {
                const inputElement = e.target;
                inputElement.style.height = (inputElement.scrollHeight + 2) + "px";
            },
            beginEdition(e) {
                this.word.editing = true;
                this.tagsString = this.word.tags.join(', ');
                this.translationsString = this.word.translations.join(', ');
            },
            saveChanges(e) {
                this.content = this.word.content;
                this.word.tags = this.tagsString
                    .split(',')
                    .filter(t => t.trim())
                    .map(t => t.trim());

                this.word.translations = this.translationsString
                    .split(',')
                    .filter(t => t.trim())
                    .map(t => t.trim());

                this.word.editing = false;
            },
            discardChanges(e) {
                this.tagsString = this.word.tags.join(', ');
                this.translationsString = this.word.translations.join(', ');
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
                
                if(from == to) {
                    clearInterval(this.animation);
                    return;
                }
                
                if(!~to.indexOf(from)){
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

        &_content_ {
            &input, &display {
                line-height: 1.5                                                                                            
            }

            &input {
                border: 1px solid transparent;
                border-bottom: 1px solid rgba(0, 0, 0, 0.23);
                width: 100%;
            }

            &display {
                border: 2px solid transparent;
                &-small {
                    font-size: 2rem;
                    line-height: 1.2;
                    margin-bottom: .5rem;
                }
            }
        }
    }

    .form-control {
        height: initial;
        margin: 0;
        overflow: hidden;
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