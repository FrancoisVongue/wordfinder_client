import axios from 'axios'
import test from './test'

axios.interceptors.response.use(response => response,
    error => {
        console.log(error);
        let err = error.response.data;
        if(err.indexOf('proxy'))
            err = "Please, check your connection to the internet."
        return Promise.reject(err);
    } );

export default {
    SignUp(user) {
        return axios({
            method: 'post',
            url: 'user',
            data: user,
        });
    },
    SignIn(credentials) {
        return axios({
            method: 'post',
            url: 'user/login',
            data: credentials,
        });
    },
    SignInWithToken() { 
        const token = localStorage.getItem('token');
        
        if(!token) {
            return Promise.reject(new Error("You have to login again :("));
        }
            
        return axios({
            method: 'get',
            url: 'user',
            headers: {
                Authorization: "Bearer " + token
            }
        });
    }
        // SignInWithToken(token) { 
        //     let result = {
        //         data: test.user,
        //         headers: {
        //             'x-token': 'sdfsdfsdfsdf'
        //         }
        //     }
        //     return Promise.resolve(result);
        // }
}


/*test*/
// export default {
//     SignUp(user) {
//         test.user.token = "sdf235325asfg242t";
//         return new Promise(_ => setTimeout(_, 400, test.user));
//     },
//     SignIn(credentials) {
//         test.user.token = "sdf235325asfg242t";
//         return Promise.resolve(test.user);
//     },
// }