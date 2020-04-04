import findWordsWindow from './addWordsWindow'
import myWordsWindow from './myWordsWindow'
import signUpWindow from "./signUpWindow";
import test from './test'
import home from '../components/addWordsWindow/addWordsWindow.vue'

import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [
        ...test,
    findWordsWindow,
    myWordsWindow,
    signUpWindow,
    { path: '*', component: home }
];

export default new VueRouter({mode: 'history', routes})