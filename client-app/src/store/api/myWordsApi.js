import axios from 'axios'

export default {
  GetWords(content, name) {
    return axios({
      method: 'get',
      url: 'words'
    });
  }
}