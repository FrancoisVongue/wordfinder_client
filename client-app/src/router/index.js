import findWordsWindow from './findWordsWindow'
import myWordsWindow from './myWordsWindow'
import test from './test'
import home from '../components/addWordsWindow/findWordsWindow.vue'

import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [
  test,                       // todo delete
  findWordsWindow,
  myWordsWindow,
  { path: '*', component: home }
];

export default new VueRouter({mode: 'history', routes})