import store from '../../../../store'

export default {
    CSVtoArray(csv) {
        return csv.split(',').filter(s => s.trim()).map(s => s.trim());
    },
    submitWords(words) {
        return store.dispatch('submitWords', words);
    },
    discardWords() {
        return store.dispatch('discardWords');
    }
}