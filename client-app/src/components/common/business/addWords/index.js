import store from '../../../../store'

export default {
    submitWords(words) {
        return store.dispatch('submitWords', words);
    },
    discardWords() {
        return store.dispatch('discardWords');
    }
}