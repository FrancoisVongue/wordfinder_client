import signInWindow from '../components/signInWindow/signInWindow'

export default {
    name: 'signIn',
    path: '/sign-in',
    component: signInWindow,
    props: (route) => ({currentWindow: route.name}),
}