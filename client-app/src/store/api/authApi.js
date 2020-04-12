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
        }).then(response => {
            const user = response.data;
            if(!user.id || !user.firstName) 
                throw new Error("Verify user: invalid response");
            return user;
        });
    }
}