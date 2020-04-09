<template>
    <div class="window">
        <p class="display-4">Login</p>
        <ValidationObserver v-slot="{ failed, handleSubmit }">
            <form @submit.prevent="handleSubmit(signIn)">
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="fname_create">Login</label>
                        <ValidationProvider name="first name" rules="alpha|required|min:5|max:14" v-slot="{errors}">
                            <input type="text" class="form-control" id="fname_create"
                                   placeholder="Enter your first name, please."
                                   v-model="login">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else> </small>
                        </ValidationProvider>
                    </div>
                </div>
                
                <div class="row">
                    <div class="form-group col-md-6">
                        <label for="password_create">Password</label>
                        <ValidationProvider name="password" rules="min:6|max:20|required" v-slot="{errors}">
                            <input type="password" class="form-control" id="password_create" placeholder="Password"
                                   v-model="password">
                            <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                            <small class="info form-text text-muted validation_info" v-else>Choose a secure password</small>
                        </ValidationProvider>
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
    import ValidationProvider from '../common/validation';
    import {ValidationObserver, setInteractionMode} from 'vee-validate';
    setInteractionMode('eager');

    export default {
        name: "signUpWindow",
        components: { ValidationProvider, ValidationObserver },
        data() {
            return {
                login: '',
                password: ''
            }
        },
        methods: {
            signIn() {
                
            }
        }
    }
</script>

<style lang="scss" scoped>
    
</style>