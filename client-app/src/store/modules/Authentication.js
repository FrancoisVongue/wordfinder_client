import api from "../api/authApi"
import addWordsApi from "../api/addWordsApi";

let state = {
    authentication: {
        authenticated: false,
        token: '',
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
        state.authentication.user = {...user};
    },
    SET_TOKEN(state, token) {
        state.authentication.token = token;
    },
    SET_AUTH_STATE(state, auth_state) {
        state.authentication.authenticated = auth_state;
    },
    SET_LEXICON(state, words) {
        state.authentication.user.words = [...words];
    }
}

let actions = {
    verifyUser({commit}) {
        const token = localStorage.getItem("token");
        if (token) {
            commit('SET_LOADING', true);
            api.VerifyUser(token)
                .then( user => {
                    commit('SET_LOADING', false);
                })
                .catch( reason => {
                    commit('SET_LOADING', false)
                });
        }
    },
    signUpUser({commit}, user) {
        commit('SET_LOADING', true);
        return api.SignUp(user)
            .then(response => {
                commit('SET_USER', response.data);
                commit('SET_TOKEN', response.headers['x-token']);
                commit('SET_AUTH_STATE', true);
                commit('SET_LOADING', false);
                return response.data;
            });
    },
    setLexicon(context, words) {
        words.map(w => {
            w.userId = context.getters('userInfo').id;
            return w;
        });
        context.commit('SET_LEXICON', words);
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