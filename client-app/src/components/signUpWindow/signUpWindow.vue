<template>
	<div class="window">
		<p class="display-4">Registration</p>
        <ValidationObserver v-slot="{ failed, handleSubmit }">
            <form @submit.prevent="handleSubmit(signUp)">

                <div class="row">
                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="firstNameField"/>
                    </div>

                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="secondNameField"/>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="emailField"/>
                    </div>

                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="loginField"/>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="passwordField"/>
                    </div>

                    <div class="form-group col-md-6">
                        <InputField :field-data.sync="passwordRepeatField"/>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12">
                        <label for="lexicon">How many words do you know?
                            (words will be taken from <a target="_blank" 
                             href="https://github.com/first20hours/google-10000-english/blob/master/google-10000-english-no-swears.txt">this place</a>)
                        </label>
                        <input type="range" class="custom-range" min="0" max="9500"
                            id="lexicon" v-model="lexiconSize" step="100">
                    </div>
                    <div class="col-md-12">
                        <p class="display-4 small-display text-center">{{lexiconSize}}</p>
                    </div>
                </div>
                
                <button type="submit" class="btn btn-primary"
                        :disabled="failed">Sign up
                </button>
            </form>
        </ValidationObserver>
	</div>
</template>

<script>
    import InputField from "../common/InputField";
    import {ValidationObserver, setInteractionMode} from 'vee-validate';
    import dictionary from './processedDictionary.js';
    import _ from 'lodash'
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationObserver, InputField },
        data() {
            return {
                firstNameField: {
                    id: 'firstName_create',
                    type: 'text',
                    smallText: '',
                    name: 'First name',
                    rules: 'alpha|required|max:20',
                    placeholder: 'Input your first name, please',
                    value: ''
                },
                secondNameField: {
                    id: 'secondName_create',
                    type: 'text',
                    smallText: '(optional)',
                    name: 'Second name',
                    rules: 'alpha|max:30',
                    placeholder: 'Input your second name, please',
                    value: ''
                },
                loginField: {
                    id: 'login_create',
                    type: 'text',
                    smallText: '',
                    name: 'Login',
                    rules: 'min:5|max:14|required',
                    placeholder: 'Input your login, please',
                    value: ''
                },
                emailField: {
                    id: 'email_create',
                    type: 'text',
                    smallText: '',
                    name: 'Email',
                    rules: 'required|email',
                    placeholder: 'Input your email, please',
                    value: ''
                },
                passwordField: {
                    id: 'password_create',
                    type: 'password',
                    smallText: '',
                    name: 'Password',
                    rules: 'diversity:5|min:6|max:20|required',
                    placeholder: 'Input your password, please',
                    value: ''
                },
                lexiconSize: 0,
            }
        },
        computed: {
            passwordRepeatField() {
                return {
                    id: 'repeat_password',
                    type: 'password',
                    smallText: '',
                    name: 'Repeat password',
                    rules: 'required|sameas:' + this.passwordField.value,
                    placeholder: 'Repeat your password, please',
                    value: ''}
            }
        },
        methods: {
            signUp() {
                const words = dictionary
                    .slice(0, this.lexiconSize)
                    .map(w => {
                        return {content: w};
                    });
                
                const user = {
                    firstName: this.firstNameField.value,
                    lastName: this.secondNameField.value,
                    login: this.loginField.value,
                    email: this.emailField.value,
                    password: this.passwordField.value,
                    words: [...words]
                }
                
                this.$store.dispatch('signUp', user)
                    .then(token => {
                        this.$store.dispatch('verifyUser', token);
                    });
            }
        }
    }
</script>

<style lang="scss" scoped>
    .small-display {
        font-size: 2rem;
    }
</style>