import axios from 'axios'
import test from './test'

// export default {
//     getMyWords(token) {
//         return axios({
//             method: 'get',
//             url: 'words',
//             headers: {'Authorization': `bearer ${token}`}
//         });
//     }
// }

/*test*/
export default {
    GetUserWords(amount) {
        const token = localStorage.getItem('token');
        return Promise.resolve(test.words);
    }
}