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
    SET_USER_INFO(state, {tags, topics, words}) {
        state.user.topics = topics;
        state.user.tags = tags;
        state.user.words = words;
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
    updateUserInfo({state, commit}, addedWords) {
        const unique = (s = new Set()) => v => s.has(v) ? false : (s.add(v), true);
        
        const uniqueTag = unique();
        const uniqueTopic = unique();
        
        const {tags, topics, words} = state.user;
        
        const info = {};
        info.tags = addedWords.map(w => w.tags).reduce((b,v) => b.concat(v), [])
            .concat(tags)
            .filter(t => uniqueTag(t.name));
        info.topics = addedWords.map(w => w.topic)
            .concat(topics)
            .filter(t => uniqueTopic(t.name));
        info.words = addedWords.concat(words)
            .map(w => ({id: w.id, content: w.content}));
            
        commit('SET_USER_INFO', info);
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
    userTopicsInfo(state) {
        return state.user.topics;
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