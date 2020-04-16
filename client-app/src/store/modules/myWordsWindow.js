import api from "../api/myWordsApi"
import test from "../api/test";

const state = {
    wordsInfo: {
        words: [],
        tags: [],
        topics: []
    },
    myWords: []
}

const mutations = {
    SET_WORDS(state, words) {
        state.myWords = [...words];
    },
    SET_INFO(state, info) {
        state.wordsInfo = info;
    }
}

const actions = {
    getMyWords(context) {
        const token = localStorage.getItem('token');
        return api.getMyWords(token)
            .then(words => {
                context.commit('SET_WORDS', words);
                return words;
            });
    },
    getWordsInfo({commit}) {
        return api.getInfo()
            .then(info => {
                commit('SET_INFO', info);
                return info;
            });
    }
}

const getters = {
    WordsInfo(state) {
        return state.wordsInfo;
    }
}

export default {state, mutations, actions, getters}