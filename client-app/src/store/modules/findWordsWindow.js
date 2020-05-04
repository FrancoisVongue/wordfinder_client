import api from "../api/addWordsApi"

let state = {
    text: {
        Name: "",
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
        state.text.foundWords = [];
    },
}

let actions = {
    getNewWords({commit}, text) {
        commit('SET_TEXT_PROPERTIES', text);
        
        const getWordsPromise = api.GetWordsFromText(text)
            .then(words => {
                commit('SET_FOUND_WORDS', words);
                return words;
            });
        
        return getWordsPromise;
    },
    submitWords({commit, state}) {
        return api.SubmitWords(state.text)
            .then(() => {
                state.words.concat(state.text.foundWords);
                commit('DISCARD_WORDS');
            });
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

export default {state, mutations, actions, getters}