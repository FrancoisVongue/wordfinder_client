import axios from 'axios'
import test from './test'

export default {
    getMyWords(token) {
        return axios({
            method: 'get',
            url: 'words/all',
            headers: {'Authorization': `bearer ${token}`}
        }).then((response) => {
            let words = response.data;
            words = words.map(w => { 
                let word = {
                    id: w.id,
                    content: w.content,
                    tags: ["no tags"],
                    translations: ["no translations"],
                    notes: 'word to greet people',
                    familiar: false,
                    editing: false,
                }
                return word;
            });
            return words;
        });
    },
    getInfo(token) {
        // return axios({
        //     method: 'get',
        //     url: 'words/info',
        //     headers: {'Authorization': `bearer ${token}`}
        // });
        return Promise.resolve(test.info);
    }
}