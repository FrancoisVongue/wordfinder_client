import addWordsWindow from './addWordsWindow'
import myWordsWindow from './myWordsWindow'
import repeatWordsWindow from './repeatWordsWindow'
import auth from "./authentication"
import store from '../store/index'
import test from './test'
import home from '../components/addWordsWindow/addWordsWindow.vue'

import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [
        ...test,
    addWordsWindow,
    myWordsWindow,
    repeatWordsWindow,
        ...auth,
    { path: '*', component: home }
];

const router = new VueRouter({mode: 'history', routes});
router.beforeEach((to, from, next) => {
    if(to.name == 'signUp')
        next();
    else if(!store.getters.authenticated && to.name != 'signIn')
        next({name: 'signIn'});
    else 
        next();
});

export default router;