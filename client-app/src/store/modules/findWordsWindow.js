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
    DISCARD_WORDS(state, words) {
        let {text} = state;
        
        if(!words)
            text.foundWords = [];
        else {
            let wordsToDiscard = new Set(words);
            text.foundWords = text.foundWords
                .filter(w => !wordsToDiscard.has(w));
        }
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
    submitWords({commit}, wordsToSubmit) {
        return api.SubmitWords(wordsToSubmit)
            .then(words => {
                commit('DISCARD_WORDS', wordsToSubmit);
                return words;
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