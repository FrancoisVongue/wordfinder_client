import api from "../api/authApi"

let state = {
    authenticated: false,
    loading: false,
    user: GetEmptyUser(),
    authentication_error: ''
}

let mutations = {
    SET_LOADING(state, isLoading) {
        state.loading = isLoading;
    },
    SET_USER(state, user) {
        state.authenticated = true;
        state.user = user;
        localStorage.setItem('token', user.token);
    },
    DISCARD_USER(state) {
        localStorage.setItem('token', null);
        state.authenticated = false;
        state.user = GetEmptyUser();
    },
    SET_USER_UNSET_LOADING(state, user) {
        state.loading = false;
        state.authenticated = true;
        state.user = user;
    },
}

let actions = {
    signInWithToken({commit, state}) {
        commit('SET_LOADING', true);
        const signInPromise = api.SignInWithToken(localStorage.getItem('token'))
            .then(user => {
                commit('SET_USER_UNSET_LOADING', user);
                return true;
            })
            .catch(_ => {
                state.authentication_error = "failed to authenticate with a token";
                return false;
            });
            
        return signInPromise;
    },
    signUp({commit}, user) {
        commit('SET_LOADING', true);
        const signUpPromise = api.SignUp(user)
            .then(user => {
                commit('SET_USER_UNSET_LOADING', user);
            });
            
        return signUpPromise;
    },
    signIn({commit}, credentials) {
        commit('SET_LOADING', true);
        const signInPromise =  api.SignIn(credentials)
            .then(user => {
                commit('SET_USER_UNSET_LOADING', user);
            });
        
        return signInPromise;
    },
    logOut({commit}) {
        return new Promise(resolve => {
            commit('DISCARD_USER');
            resolve();
        });
    }
}

let getters = {
    authLoading(state) {
        return state.loading;
    },
    authenticated(state) {
        return state.authenticated;
    },
    userWordsInfo(state) {
        return state.user.words;
    },
    userName(state) {
        return state.user.firstName;
    }
}

function GetEmptyUser() {
    return {
        id: null,
        token: null,
        firstName: '',
        lastName: '',
        login: '',
        email: '',
        topics: [],
        tags: [],
        words: []
    }
}

export default {state, mutations, actions, getters}