<template>
	<div class="window">
		<p class="display-4">Registration</p>
        <ValidationObserver v-slot="{ failed, handleSubmit }">
            <form @submit.prevent="handleSubmit(signUp)">

                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="fname_create">First Name</label>
                        <ValidationProvider name="first name" rules="alpha|required" v-slot="{errors}">
                            <input type="text" class="form-control" id="fname_create"
                                   placeholder="Enter your first name, please."
                                   v-model="firstName">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else> </small>
                        </ValidationProvider>
                    </div>

                    <div class="form-group col-md-6">
                        <label for="sname_create">Second Name</label>
                        <ValidationProvider name="second name" rules="alpha" v-slot="{errors}">
                            <input type="text" class="form-control" id="sname_create" 
                                   placeholder="Enter your second name"
                                   v-model="secondName">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else>
                                (optional)
                            </small>
                        </ValidationProvider>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="email_create">Email</label>
                        <ValidationProvider name="email" rules="email|required" v-slot="{errors}">
                            <input type="text" class="form-control" id="email_create"
                                   placeholder="Enter your email"
                                   v-model="email">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else> </small>
                        </ValidationProvider>
                    </div>

                    <div class="form-group col-md-6">
                        <label for="login_create">Login</label>
                        <ValidationProvider name="login" rules="min:5|max:14|required" v-slot="{errors}">
                            <input type="text" class="form-control" id="login_create" placeholder="Enter your login"
                                   v-model="login">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else> </small>
                        </ValidationProvider>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="password_create">Password</label>
                        <ValidationProvider name="password" rules="diversity:5|min:6|max:20|required" v-slot="{errors}">
                            <input type="password" class="form-control" id="password_create" placeholder="Password"
                                   v-model="password">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else>Choose a secure password</small>
                        </ValidationProvider>
                    </div>
    
                    <div class="form-group col-md-6">
                        <label for="repeat_password">Repeat password</label>
                        <ValidationProvider name="password" :rules="'required|sameas:' + password" v-slot="{errors}">
                            <input type="password" class="form-control" id="repeat_password" placeholder="Repeat password"
                                   v-model="password_repeat">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else> </small>
                        </ValidationProvider>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12">
                        <label for="lexicon">How many words do you know?
                            (words will be taken from <a target="_blank" 
                             href="https://github.com/first20hours/google-10000-english/blob/master/google-10000-english-no-swears.txt">this place</a>)
                        </label>
                        <input type="range" class="custom-range" min="0" max="9500" id="lexicon"
                               v-model="lexiconSize"
                                step="100">
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
    import ValidationProvider from '../common/validation';
    import {ValidationObserver, setInteractionMode} from 'vee-validate';
    import dictionary from './processedDictionary';
    import _ from 'lodash'
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationProvider, ValidationObserver },
        data() {
            return {
                firstName: '',
                secondName: '',
                login: '',
                email: '',
                password: '',
                password_repeat: '',
                words: [],
                lexiconSize: 0,
            }
        },
        methods: {
            signUp() {
                this.words = JSON.parse(dictionary)
                    .slice(0, this.lexiconSize)
                    .map(w => {content: w});
                const user = _.omit(this, ['password_repeat','lexiconSize']);
                this.$store.dispatch('signUpUser', user);
            }
        }
    }
</script>

<style lang="scss" scoped>
    .small-display {
        font-size: 2rem;
    }
</style>