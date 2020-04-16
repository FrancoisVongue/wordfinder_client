import api from "../api/addWordsApi"

let state = {
    text: {
        Name: "Star Wars",
        Content: "",
        foundWords: [
            
        ]
    },
}

let mutations = {
    SET_TEXT_PROPERTIES(state, text) {
        state.text.Name = text.Name;
        state.text.Content = text.Content;
    },
    SET_FOUND_WORDS(state, foundWords) {
        state.text.foundWords = foundWords;
    },
    DISCARD_WORDS(state) {
        clearState(state);
    },
}

let actions = {
    getNewWords({commit}, text) {
        commit('SET_TEXT_PROPERTIES', text);
        
        if(userAuthenticated()) {
            const token = getToken();
            const getWordsPromise = api.GetWordsFromText(text, token)
                .then(words => {
                    const newWords = words.map(word => NewWordFromContent(word));
                    commit('SET_FOUND_WORDS', newWords);
                });
            
            return getWordsPromise;
        }
        else {
            return Promise.reject(new Error("User is not authenticated"));
        }
    },
    submitWords({commit, state}) {
        if(userAuthenticated())
            return api.SubmitWords(state.text)
                .then(() => {
                    commit('DISCARD_WORDS')
                });
        else 
            return Promise.reject("User is not authenticated")
    },
    discardWords({commit}, text) {
        return Promise.resolve(commit('DISCARD_WORDS'));
    },
}

let getters = {
    foundWords(state) {
        return state.text.foundWords;
    },
    textName(state) {
        return state.text.Name;
    }
}

function clearState(state) {
    for (const prop in state.text) {
        state.text[prop] = state.text[prop].slice(0, 0);
    }
}

function NewWordFromContent(content) {
    return {content: content, editing: true};
}

function userAuthenticated() {
    const token = getToken();
    return token != 'undefinded';
}

function getToken() {
    return localStorage.getItem('token');
}

export default {state, mutations, actions, getters}