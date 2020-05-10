<template>
    <div class="window">
        <p class="display-4">Login</p>
        <error-message :content="errorMessage"/>
        <ValidationObserver v-slot="{ failed, handleSubmit }">
            <form @submit.prevent="handleSubmit(signIn)">
                <div class="row">
                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="loginField"/>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="passwordField"/>
                    </div>
                </div>
                <button type="submit" class="btn btn-primary"
                        :disabled="failed">Sign in
                </button>
            </form>
        </ValidationObserver>
    </div>
</template>

<script>
    import InputField from "../common/InputField"
    import errorMessage from '@/components/common/errorMessage'
    import fieldConfig from "../common/fieldConfig/signIn"
    import business from "../common/business/signIn"
    import {ValidationObserver, setInteractionMode} from 'vee-validate'
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationObserver, InputField, errorMessage },
        data() {
            return {
                loginField: fieldConfig.loginField,
                passwordField: fieldConfig.passwordField,
                errorMessage: ''
            }
        },
        methods: {
            signIn() {
                business.signIn()
                    .then(_ => this.$router.push({name: "myWords"}))
                    .catch(errorMessage => {
                        this.errorMessage = errorMessage;
                    });
            } 
        }
    }
</script>

<style lang="scss" scoped>
    
</style>