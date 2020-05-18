<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
        <p class="display-4">Find new words!</p>
        <error-message :content="errorMessage"/>
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
                    <span class="spinner-grow spinner-grow-sm"
                        role="status" aria-hidden="true">
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
    import fieldConfig from '../common/fieldConfig/findWords'
    import errorMessage from '@/components/common/errorMessage'

    export default {
        name: "findWords",
        data() {
            return {
                NameField: fieldConfig.NameField,
                ContentField: fieldConfig.ContentField,                
                loading: false,
                errorMessage: ''
            }
        },
        methods: {
            getWords() {
                this.errorMessage = '';
                this.loading = true;
                const text = {
                    Name: this.NameField.value,
                    Content: this.ContentField.value
                }
                this.$store.dispatch('getNewWords', text)
                    .then(wordsAmount => {
                        if(wordsAmount > 0)
                            this.$router.push({name: 'addWords'});
                        else 
                            this.errorMessage = 'No new words were found!'
                    })
                    .finally(() => {
                        this.loading = false;
                    })
            }
        },
        components: { ValidationObserver, InputField, errorMessage },
        
    }
</script>

<style lang="scss" >
    .text-content_input textarea{
        height: 7em;
    }
</style>