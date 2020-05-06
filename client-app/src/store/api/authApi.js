import axios from 'axios'
import test from './test'

axios.interceptors.response.use(response => response,
    error => Promise.reject(error.response) );

// export default {
//     SignUp(user) {
//         return axios({
//             method: 'post',
//             url: 'user',
//             data: user,
//         });
//     },
//     SignIn(credentials) {
//         return axios({
//             method: 'post',
//             url: 'user/signin',
//             data: credentials,
//         }).then(response => response.headers["x-token"]);
//     },
//     VerifyUser(token) {
//         return axios({
//             method: 'post',
//             url: 'user/verify',
//             headers: {'Authorization': `bearer ${token}`}
//         });
//     }
// }


/*test*/
export default {
    SignUp(user) {
        test.user.token = "sdf235325asfg242t";
        return new Promise(_ => setTimeout(_, 400, test.user));
    },
    SignIn(credentials) {
        test.user.token = "sdf235325asfg242t";
        return Promise.resolve(test.user);
    },
    SignInWithToken(token) { 
        test.user.token = "sdf235325asfg242t";
        return Promise.resolve(test.user);
    }
}