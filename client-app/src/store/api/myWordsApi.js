import axios from 'axios'
import test from './test'

export default {
    getMyWords(descriptor, token) {
        return Promise.resolve(test.words);
    }
}