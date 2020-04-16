import axios from 'axios'

export default {
    SignUp(user) {
        return axios({
            method: 'post',
            url: 'user/register',
            data: user,
        });
    },
    SignIn(credentials) {
        return axios({
            method: 'post',
            url: 'user/signin',
            data: credentials,
        }).then(response => response.headers["x-token"]);
    },
    VerifyUser(token) {
        return axios({
            method: 'post',
            url: 'user/verify',
            headers: {'Authorization': `bearer ${token}`}
        });
    }
}