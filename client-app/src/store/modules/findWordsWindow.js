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
    SET_TEXT(state, text) {
        state.text.Name = text.Name;
        state.text.Content = text.Content;
    },
    SET_FOUND_WORDS(state, foundWords) {
        state.text.foundWords = foundWords;
    },
    DISCARD_WORDS({text}) {
        text.foundWords = [];
    },
}

let actions = {
    getNewWords({commit}, text) {
        commit('SET_TEXT', text);
        
        const getWordsPromise = api.GetWordsFromText(text)
            .then(({data}) => {
                commit('SET_FOUND_WORDS', data);
                return data.length;
            });
        
        return getWordsPromise;
    },
    getWordsForRepetition() {
        return api.getWordsForRepetition()
            .then(({data}) => {
                return data;
            });
    },
    repeatWords(_, words) {
        return api.repeatWords(words)
            .then(({data}) => {
                return data;
            });
    },
    submitWords({commit}, wordsToSubmit) {
        return api.SubmitWords(wordsToSubmit)
            .then(({data}) => {
                commit('DISCARD_WORDS');
                return data;
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