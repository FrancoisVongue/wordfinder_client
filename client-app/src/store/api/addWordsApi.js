import axios from 'axios'
import test from './test'

export default {
    GetWordsFromText(text) {
        return axios({
            method: 'post',
            url: 'text/words',
            data: text,
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    getWordsForRepetition() {
        return axios({
            method: 'get',
            url: '/words/repeat',
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    repeatWords(words) {
        return axios({
            method: 'post',
            url: '/words/repeat',
            data: words.map(w => w.id),
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    SubmitWords(words) {
        return axios({
            method: 'post',
            url: 'words',
            data: words,
            headers: {
                Authorization: `Bearer ${localStorage.getItem('token')}`
            }
        });
    },
    GetShallowInfo(token) {
        return axios({
            method: 'post',
            url: 'user/get-lexicon',
            headers: {'Authorization': `bearer ${token}`}
        });
    }
}

/*test*/
// export default {
//     GetWordsFromText() {
//         return Promise.resolve(test.wordsFromText);
//     },
//     SubmitWords() {
//         return Promise.resolve(test.wordsFromText);
//     }
// }