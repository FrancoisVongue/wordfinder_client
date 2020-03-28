import api from "../../api/addWordsApi"

let state = {
  text: {
    Name: "",
    Content: "",
    foundWords: []
  },
}

let mutations = {
  UPDATE_WORDS(state, foundWords) {
    state.text.foundWords = foundWords;
  },
  DISCARD_WORDS(state) {
    for(const property in state.text) {
      state.text[property] = state.text[property].slice(0,0);
    }
  },
}

let actions = {
  updateWords({commit}, text) {
    api.GetWordsFromText(text)
      .then(response => {
        commit('UPDATE_WORDS', response.data)
      });
  },
  submitWords({commit}, text) {
    api.GetWords(text)
      .then(response => {
        commit('UPDATE_WORDS', response.data)
      });
  },
  discardWords({commit}, text) {
    return new Promise.resolve(commit('DISCARD_WORDS'));
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