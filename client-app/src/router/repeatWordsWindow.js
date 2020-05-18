import repeatWordsWindow from '../components/repeatWordsWindow/repeatWordsWindow.vue'

export default { 
    name: 'repeatWords',
    path: '/repeat-words',
    component: repeatWordsWindow,
    props: (route) => ({currentWindow: route.name}),
}