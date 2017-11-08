import Vue from 'vue'
import VueRouter from 'vue-router'
import SuiVue from 'semantic-ui-vue';
import 'semantic-ui-css/semantic.min.css';

import App from '../../Vue Components/SemanticApp.vue'
import Home from '../../Vue Components/Home.vue'
import Customers from '../../Vue Components/Customers.vue'
import Incidents from '../../Vue Components/Incidents.vue'

Vue.use(VueRouter)
Vue.use(SuiVue)

const routes = [
    { path: '/home', component: Home },
    { path: '/customers', component: Customers },
    { path: '/incidents', component: Incidents },
    { path: '', redirect: '/home' },
    { path: '/', redirect: '/home' }
]

const router = new VueRouter({
    routes // short for `routes: routes`
})

new Vue({
  el: '#app',
  template: '<App/>',
  components: {
      App
  },
  router
})
