import api from "../api/authApi"

let state = {
    authenticated: false,
    loading: false,
    user: {
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

let mutations = {
    SET_LOADING(state, isLoading) {
        state.loading = isLoading;
    },
    SET_TOKEN(_, token) {
        localStorage.setItem('token', token);  
    },
    SET_USER(state, user) {
        state.authenticated = true;
        state.user = user;
    },
    DISCARD_USER(state) {
        localStorage.clear('token');
        state.authenticated = false;
        state.user = GetEmptyUser();
    }
}

let actions = {
    signInWithToken({commit}) {
        commit('SET_LOADING', true);
        const signInPromise = api.SignInWithToken()
            .then(({data: user}) => {
                commit('SET_USER', user);
                return user;
            })
            .catch(response => {
                throw new Error(response.message);
            })
            .finally(_ => {
                commit('SET_LOADING', false);
            });
            
        return signInPromise;
    },
    signUp({commit}, user) {
        commit('SET_LOADING', true);
        const signUpPromise = api.SignUp(user)
            .then(({data: user, headers}) => {
                commit('SET_USER', user);
                commit('SET_TOKEN', headers['x-token']);
                return user;
            })
            .finally(_ => {
                commit('SET_LOADING', false);
            });
            
        return signUpPromise;
    },
    signIn({commit}, credentials) {
        commit('SET_LOADING', true);
        const signInPromise =  api.SignIn(credentials)
            .then(({data: user, headers}) => {
                commit('SET_USER', user);
                commit('SET_TOKEN', headers['x-token']);
                return user;
            })
            .finally(_ => {
                commit('SET_LOADING', false);
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
    },
    userInfo(state) {
        return state.user;
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