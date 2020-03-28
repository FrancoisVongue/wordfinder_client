import api from "../../api/myWordsApi"

let state = {
  foundWords: []
}

let mutations = {
  UPDATE_WORDS(state, foundWords) {
    state.foundWords = foundWords;
  }
}

let actions = {
}

let getters = {
}

export default {state, mutations, actions, getters}