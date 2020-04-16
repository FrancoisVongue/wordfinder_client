<template>
    <div id="app" class="container">
        <header class="menu">
            <nav class="navbar navbar-expand-lg navbar-light bg-light">
                <router-link to="/" class="navbar-brand">WordFinder</router-link>
                <button class="navbar-toggler" type="button" data-toggle="collapse"
                        data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav mr-auto">
                        <li class="{nav-item: true, active: currentWindow == supplyText}">
                            <router-link class="nav-link" :to="{name: 'supplyText'}">
                                Find words
                            </router-link>
                        </li>
                        <li class="{nav-item: true, active: currentWindow == myWords}">
                            <router-link class="nav-link" :to="{name: 'myWords'}">
                                My words
                                <span class="badge badge-primary" v-if="userInfo.words && userInfo.words.length > 0">
                                    {{userInfo.words.length}}
                                </span>
                            </router-link>
                        </li>
                    </ul>
                    
                    <ul class="navbar-nav">
                        <template v-if="!authenticated">
                            <li class="{nav-item: true, active: currentWindow == signUp}">
                                <router-link class="nav-link" :to="{name: 'signUp'}">
                                    Sign Up
                                </router-link>
                            </li>
                            <li class="{nav-item: true, active: currentWindow == signIn}">
                                <router-link class="nav-link" :to="{name: 'signIn'}">
                                    Sign In
                                </router-link>
                            </li>
                        </template>
                        <template v-else>
                            <li class="nav-item">
                                <router-link class="nav-link" :to="{name: 'myWords'}">
                                    {{userInfo.firstName}}
                                </router-link>
                            </li>
                            <li class="nav-item">
                                <router-link class="nav-link" :to="{name: 'logOut'}">
                                    Log out
                                </router-link>
                            </li>
                        </template>
                    </ul>
                </div>
            </nav>
        </header>

        <main class="currentWindow" v-if="!authLoading">
            <router-view></router-view>
        </main>
        <main class="currentWindow" v-else>
            <div class="d-flex justify-content-center auth_spinner align-items-center">
                <div class="spinner-grow" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
        </main>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex'
    export default {
        name: 'app',
        props: ['currentWindow'],
        beforeCreate() {
            this.$store.dispatch("verifyUser")
                .then(auth => {
                    if (auth) this.$router.push({name: 'myWords'});
                    console.log('logged - ' + auth);
                });
        },
        computed: {
            ...mapGetters(['authLoading', 'authenticated', 'userInfo'])
        },
        methods: {
        }
    }
</script>

<style lang="scss">
    .window {
        margin: 1em 0;
    }

    .error {
        color: orangered;
    }
    
    .auth_spinner {
        height: 6rem;
    }
</style>
