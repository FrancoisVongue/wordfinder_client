import axios from 'axios'

export default {
  GetWordsFromText(text) {
    return axios({
      method: 'post',
      url: 'text',
      data: text
    }).then(({data}) => data);
  },
  SubmitWords(text) {
    return axios({
      method: 'post',
      url: 'words',
      data: text
    });
  },
}