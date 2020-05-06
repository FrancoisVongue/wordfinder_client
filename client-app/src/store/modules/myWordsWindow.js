import api from "../api/myWordsApi"

const state = {
    words: []
}

const mutations = {
    SET_WORDS(state, words) {
        state.words = words;
    }
}

const actions = {
    getMyWords(context) {
        const wordsToShow = 25;
        const wordsPromise = api.GetUserWords(wordsToShow)
            .then(words => {
                context.commit('SET_WORDS', words);
                return words;
            });
        
        return wordsPromise;
    }
}

const getters = {}

export default {state, mutations, actions, getters}