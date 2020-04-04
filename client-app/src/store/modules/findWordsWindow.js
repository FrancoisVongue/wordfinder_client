import api from "../api/addWordsApi"

let state = {
  text: {
    Name: "",
    Content: "",
    foundWords: []
  },
}

let mutations = {
  SET_TEXT(state, text) {
    state.text.Name = text.Name;
    state.text.Content = text.Content;
  },
  ADD_WORDS(state, foundWords) {
    state.text.foundWords = foundWords;
  },
  SUBMIT_WORDS(state) {
    clearState(state);
  },
  DISCARD_WORDS(state) {
    clearState(state);
  },
}

let actions = {
  getNewWords({commit}, text) {
    commit('SET_TEXT',text);
    return api.GetWordsFromText(text)
          .then(words => {
            const newWords = words.map(word => NewWordFromContent(word));
            commit('ADD_WORDS', newWords);
          });
  },
  submitWords({commit, state}, text) {
    api.SubmitWords(state.text)
      .then(() => { commit('SUBMIT_WORDS') });
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

function clearState(state) {
  for(const prop in state.text) {
    state.text[prop] = state.text[prop].slice(0,0);
  }
}

function NewWordFromContent(content) {
  return {content: content, editing: true};
}

export default {state, mutations, actions, getters}