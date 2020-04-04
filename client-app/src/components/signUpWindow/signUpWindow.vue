<template>
    <div class="window">
        <p class="display-4">Registration</p>
        <form>
            <div class="form-group">
                <label for="login_create">Login</label>
                <ValidationProvider name="login" rules="min:5|max:14" v-slot="{errors}">
                    <input type="text" class="form-control" id="login_create" placeholder="Enter your login"
                    v-model="login">
                    <small class="info form-text error" v-if="errors[0]">{{errors[0]}}</small>
                    <small class="info form-text text-muted" v-else>We'll never share your email with anyone else.</small>
                </ValidationProvider>
            </div>
            <div class="form-group">
                <label for="password_create">Password</label>
                <ValidationProvider name="password" rules="diversity:5|min:6|max:20" v-slot="{errors}">
                    <input type="password" class="form-control" id="password_create" placeholder="Password"
                    v-model="pass">
                    <small class="info form-text error" v-if="errors[0]">{{errors[0]}}</small>
                    <small class="info form-text text-muted" v-else>Choose a secure password</small>
                </ValidationProvider>
            </div>
            <div class="form-group">
                <label for="repeat_password">Repeat password</label>
                <input type="password" class="form-control" id="repeat_password" placeholder="Repeat password"
                v-model="repass">
                <small class="info form-text text-muted">We'll never share your email with anyone else.</small>
            </div>
            <button type="submit" class="btn btn-primary" @click="signUp">Sign Up</button>

        </form>
    </div>
</template>

<script>
    import ValidationProvider from './validation';

    export default {
        name: "signUpWindow",
        components: { ValidationProvider },
        data() {
            return {
                login: '',
                pass: '',
                repass: ''
            }
        },
        methods: {
            signUp() {

            }
        }
    }
</script>

<style lang="scss" scoped>
    .error {
        color: orangered;
    }
</style>