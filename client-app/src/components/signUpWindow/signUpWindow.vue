<template>
	<div class="window">
		<p class="display-4">Registration</p>
        <error-message :content="errorMessage"/>
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
                            (words will be taken from 
                            <a target="_blank" href="https://github.com/first20hours/google-10000-english/blob/master/google-10000-english-no-swears.txt">this place</a>)
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
    import errorMessage from '@/components/common/errorMessage'
    import fieldConfig from "../common/fieldConfig/signUp"
    import business from "../common/business/signUp"
    import Vue from 'vue'
    import {ValidationObserver, setInteractionMode} from 'vee-validate';
    import dictionary from './processedDictionary.js';
    import _ from 'lodash'
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationObserver, InputField, errorMessage },
        data() {
            return {
                firstNameField: fieldConfig.firstNameField,
                secondNameField: fieldConfig.secondNameField,
                loginField: fieldConfig.loginField,
                emailField: fieldConfig.emailField,
                passwordField: fieldConfig.passwordField,
                lexiconSize: 100,
                errorMessage: ''
            }
        },
        computed: {
            passwordRepeatField: _ => fieldConfig.passwordRepeatField,
        },
        methods: {
            signUp() {
                business.signUp(this.lexiconSize)
                    .then(_ => this.$router.push({name: "myWords"}))
                    .catch(errorMessage => {
                        this.errorMessage = errorMessage;
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