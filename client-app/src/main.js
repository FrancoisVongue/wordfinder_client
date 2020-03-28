import Vue from 'vue'
import App from './App.vue'
import store from './store/index.js'
import router from './router/index.js'

new Vue({
  store,
  router,
  render: h => h(App),
}).$mount('#app')
