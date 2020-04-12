import api from "../api/myWordsApi"

const state = {
    myWords: []
}

const mutations = {
    SET_WORDS(state, words) {
        state.myWords = [...words];
    }
}

const actions = {
    getMyWords({commit}) {
        return api.getMyWords()
            .then(words => {
                commit('SET_WORDS', words);
                return words;
            });
    }
}

const getters = {}

export default {state, mutations, actions, getters}