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
            .then(({data}) => {
                context.commit('SET_WORDS', data);
                return data;
            });
        
        return wordsPromise;
    },
    searchMyWords(context, config) {
        const wordsPromise = api.GetSpecificWords(config)
            .then(({data}) => {
                context.commit('SET_WORDS', data);
                return data;
            });
        
        return wordsPromise;
    },
    editWord(_, word) {
        const wordsPromise = api.EditWord(word)
            .then(({data}) => {
                return data;
            });
        
        return wordsPromise;
    }
}

const getters = {}

export default {state, mutations, actions, getters}