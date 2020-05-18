import findWordsWindow from '../components/addWordsWindow/addWordsWindow.vue'
import findWords from '../components/addWordsWindow/findWords.vue'
import addTranslation from '../components/addWordsWindow/addWords.vue'
import store from '../store/index'

export default {
    path: '/',
    component: findWordsWindow,
    props: (route) => ({currentWindow: route.name}),
    children: [
        {
            name: 'addWords',
            path: 'add-words',
            component: addTranslation,
        },
        {
            name: 'supplyText',
            path: '',
            component: findWords,
            beforeEnter(to, from, next) {
                if(store.getters.foundWords.length > 0)
                    next('/add-words');
                else 
                    next();
            }
        }
    ]
}