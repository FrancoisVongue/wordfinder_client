import axios from 'axios'

export default {
  GetWordsFromText(text) {
    return axios({
      method: 'post',
      url: 'text',
      data: text
    });
  },
  SubmitWords(text) {
    return axios({
      method: 'post',
      url: 'words',
      data: text
    });
  },
}