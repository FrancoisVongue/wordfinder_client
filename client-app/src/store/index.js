import findWordsWindow from "./modules/findWordsWindow.js"
import myWordsWindow from "./modules/myWordsWindow.js"

import Vuex from 'vuex'
import Vue from 'vue'
import actions from './actions.js'
import mutations from './mutations.js'
Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        count: 0
    },
    actions,
    mutations,
    getters : {
        
    },
    modules: { findWordsWindow, myWordsWindow } // modules 
});