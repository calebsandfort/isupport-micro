import Vue from 'vue'
import VueRouter from 'vue-router'
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
//import "babel-core/register"
//import "babel-polyfill"
//import { TableComponent, TableColumn } from 'vue-table-component';
import '@progress/kendo-ui'
import '@progress/kendo-theme-bootstrap/dist/all.css'
//import { KendoGrid, KendoGridInstaller } from '@progress/kendo-grid-vue-wrapper'
//import { KendoDataSource, KendoDataSourceInstaller } from '@progress/kendo-datasource-vue-wrapper'

//import { ServerTable, ClientTable, Event } from 'vue-tables-2';

import App from '../../Vue Components/App.vue'
import Home from '../../Vue Components/Home.vue'
import Customers from '../../Vue Components/Customers.vue'
import Incidents from '../../Vue Components/Incidents.vue'

Vue.use(VueRouter)
//Vue.use(ClientTable);
//Vue.use(ServerTable);

//TableComponent.settings({
//    tableClass: 'table table-striped table-bordered table-hover',
//    theadClass: 'thead-light',
//    tbodyClass: '',
//    filterPlaceholder: 'Filter table…',
//    filterNoResults: 'There are no matching rows',
//});

//Vue.component('table-component', TableComponent);
//Vue.component('table-column', TableColumn);
//Vue.use(KendoGridInstaller)
//Vue.use(KendoDataSourceInstaller)

let originalVueComponent = Vue.component
Vue.component = function (name, definition) {
    if (name === 'bFormCheckboxGroup' || name === 'bCheckboxGroup' ||
        name === 'bCheckGroup' || name === 'bFormRadioGroup') {
        definition.components = { bFormCheckbox: definition.components[0] }
    }
    originalVueComponent.apply(this, [name, definition])
}
Vue.use(BootstrapVue)
Vue.component = originalVueComponent

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
      App,
      //KendoGrid,
      //KendoDataSource
  },
  router
})
