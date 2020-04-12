<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
        <p class="display-4">Find new words!</p>
        <small class="info form-text error validation_info" v-if="errorMessage.length > 0">{{errorMessage}}</small>
        <form @submit.prevent="handleSubmit(getWords)">
            <div class="row">
                <div class="form-group col-md-6">
                    <InputField :field-data.sync="NameField"/>
                </div>
                
                <div class="form-group col-md-12">
                    <InputField class="text-content_input" :field-data.sync="ContentField"/>
                </div>
            </div>
            
            <button class="btn btn-primary" :disabled="failed">
                <span v-if="!loading">Submit</span>
                <template v-else>
                    <span>Loading...</span>
                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true">
                    </span>
                </template>
            </button>
        </form>
    </ValidationObserver>
</template>

<script>
    import {mapGetters} from 'vuex'
    import InputField from '../common/InputField'
    import {ValidationObserver} from 'vee-validate'

    export default {
        name: "findWords",
        data() {
            return {
                NameField: {
                    id: 'text-name_input',
                    type: 'text',
                    smallText: '',
                    name: 'Text name',
                    rules: 'alpha|required|min:3',
                    placeholder: 'Input the name of the text, plese',
                    value: ''
                },
                ContentField: {
                    id: 'text-content_input',
                    type: 'textarea',
                    smallText: 'content of the text',
                    name: 'Text content',
                    rules: 'required|min:4',
                    placeholder: 'Input text, please',
                    value: ''
                },                
                loading: false,
                errorMessage: ''
            }
        },
        methods: {
            getWords() {
                this.loading = !this.loading;
                this.$store.dispatch('getNewWords', this.text)
                    .then(() => {
                        this.loading = false;
                        this.errorMessage = '';
                        this.$router.push({name: 'addTranslation'});
                    })
                    .catch(() => {
                        this.loading = false;
                        this.errorMessage = 'Check your internet connection, please';
                    });
            }
        },
        components: { ValidationObserver, InputField },
        
    }
</script>

<style lang="scss" >
    .text-content_input textarea{
        height: 7em;
    }
</style>