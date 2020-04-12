import signUpWindow from '../components/signUpWindow/signUpWindow'
import signInWindow from '../components/signInWindow/signInWindow'
import store from '../store/index'

export default [
    {
        name: 'signUp',
        path: '/sign-up',
        component: signUpWindow,
        props: (route) => ({currentWindow: route.name})
    },
    {
        name: 'signIn',
        path: '/sign-in',
        component: signInWindow,
        props: (route) => ({currentWindow: route.name}),
    },
    {
        name: 'logOut',
        path: '/log-out',
        beforeEnter: (to, from, next) => {
            store.dispatch('logOut')
                .then(() => next({name: 'signIn'}));
        }
    }]