<template>
    <div class="window">
        <p class="display-4">Login</p>
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
    import _ from 'lodash'
    import InputField from "../common/InputField";
    import {ValidationObserver, setInteractionMode} from 'vee-validate';
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationObserver, InputField },
        data() {
            return {
                loginField: {
                    id: 'login_input',
                    type: 'text',
                    smallText: '',
                    name: 'Login',
                    rules: 'alpha|required|min:5|max:14',
                    placeholder: 'Input your login, please',
                    value: ''
                },
                passwordField: {
                    id: 'password_input',
                    type: 'password',
                    smallText: '',
                    name: 'Password',
                    rules: 'required|min:6|max:20',
                    placeholder: 'Input your password, please',
                    value: ''
                }
            }
        },
        beforeRouteEnter (to, from, next) {
            next(vm => {
                if(vm.$store.getters.authenticated)
                    next({name: 'myWords'});
                else
                    next();
            })
        },
        methods: {
            signIn() {
                const credentials = {
                    login: this.loginField.value,
                    password: this.passwordField.value
                };
                
                this.$store.dispatch('signIn', credentials)
                    .then(token => {
                        this.$router.push({name:'myWords'});
                        this.$store.dispatch('verifyUser', token);
                    });
            }
        }
    }
</script>

<style lang="scss" scoped>
    
</style>