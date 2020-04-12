import axios from 'axios'

export default {
    GetWordsFromText(text, token) {
        return axios({
            method: 'post',
            url: 'words',
            data: text,
            headers: {'Authorization': `bearer ${token}`}
        }).then(({data}) => data);
    },
    SubmitWords(text, token) {
        return axios({
            method: 'post',
            url: 'words/text',
            data: text,
            headers: {'Authorization': `bearer ${token}`}
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