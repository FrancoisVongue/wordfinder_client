import axios from 'axios'

export default {
    SignUp(user) {
        return axios({
            method: 'post',
            url: 'user/register',
            data: user,
        });
    },
    SignIn(login, password) {
        return axios({
            method: 'post',
            url: 'user/signin',
            data: JSON.stringify({login, password})
        }).then(({data}) => data);
    },
    VerifyUser(user) {
        return axios({
            method: 'post',
            url: 'user/verify',
            data: user,
            headers: {'Authorization': `bearer ${user.token}`}
        })
        .then(response => {
            if(response.status >= 400)
                throw new Error(`Couldn't authenticate the user with login ${user.login}`);
            return response.data;
        })
        .catch(e => {
            console.log(`Bad connection to the server, try again later, please!`);
        });
    }
}