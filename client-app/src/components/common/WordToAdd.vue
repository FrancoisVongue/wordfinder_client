<template>
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
</template>

<script>
    import InputField from '../common/InputField'
    import fieldConfig from '../common/fieldConfig/wordToAdd'
    import { ValidationObserver } from 'vee-validate'
    
    export default {
        name: 'Word',
        props: ['word'],
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