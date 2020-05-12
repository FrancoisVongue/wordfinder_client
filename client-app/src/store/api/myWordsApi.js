import axios from 'axios'
import test from './test'

export default {
    GetUserWords(words) {
        return axios({
            method: 'get',
            url: 'words',
            headers: {'Authorization': `bearer ${localStorage.getItem('token')}`}
        });
    },
    GetSpecificWords(config) {
        return axios({
            method: 'post',
            url: 'words/specific',
            headers: {'Authorization': `bearer ${localStorage.getItem('token')}`},
            data: config
        });
    }
}