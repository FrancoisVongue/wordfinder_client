import api from "../api/authApi"
import addWordsApi from "../api/addWordsApi";

let state = {
    authentication: {
        authenticated: false,
        loading: false,
        user: {
            id: 0,
            firstName: '',
            secondName: '',
            login: '',
            email: '',
            words: []
        }
    }
}

let mutations = {
    SET_LOADING(state, isLoading) {
        state.authentication.loading = isLoading;
    },
    SET_USER(state, user) {
        const userState = state.authentication.user;
        for(const prop in userState) {
            if(!(userState[prop] instanceof Object))
                userState[prop] = user[prop];
        }
    },
    DISCARD_USER(state) {
        const defaultUser = {
            id: 0,
            firstName: '',
            secondName: '',
            login: '',
            email: '',
            words: []
        };
        state.authentication.user = {...defaultUser};
    },
    SET_TOKEN(state, token) {
        localStorage.setItem("token", token);
    },
    SET_AUTH_STATE(state, auth_state) {
        state.authentication.authenticated = auth_state;
    },
    SET_LEXICON(state, words) {
        state.authentication.user.words = [...words];
    }
}

let actions = {
    verifyUser({commit}, jwt) {
        const token = jwt || localStorage.getItem("token");
        if (token != "undefined") {
            commit('SET_LOADING', true);
            return api.VerifyUser(token)
                .then( response => {
                    commit('SET_TOKEN', token);
                    commit('SET_USER', response.data);
                    commit('SET_AUTH_STATE', true);
                    commit('SET_LOADING', false);
                    return true;
                }) 
                .catch( reason => {
                    console.log(`invalid verification: ${reason.message}`);
                    commit('SET_TOKEN', undefined);
                    commit('DISCARD_USER');
                    commit('SET_AUTH_STATE', false);
                    commit('SET_LOADING', false);
                });
        } 
        return Promise.resolve(false);
    },
    signUp({commit}, user) {
        commit('SET_LOADING', true);
        return api.SignUp(user)
            .then(response => {
                commit('SET_LOADING', false);
                return response.data;
            })
            .catch(reason => console.log(user));
    },
    signIn({commit}, credentials) {
        commit('SET_LOADING', true);
        return api.SignIn(credentials)
            .then(token => {
                commit('SET_LOADING', false);
                return token;
            });
    },
    logOut({commit}) {
        return new Promise((res, rej) => {
            commit('SET_TOKEN', undefined);
            commit('DISCARD_USER');
            commit('SET_AUTH_STATE', false);
            res();
        });
    }
}

let getters = {
    authLoading(state) {
        return state.authentication.loading;
    },
    authenticated(state) {
        return state.authentication.authenticated;
    },
    userInfo(state) {
        return state.authentication.user;
    }
}

export default {state, mutations, actions, getters}