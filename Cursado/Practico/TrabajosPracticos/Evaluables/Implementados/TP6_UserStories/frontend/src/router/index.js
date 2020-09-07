import Vue from 'vue'
import VueRouter from 'vue-router'
import Main from '../views/Main'
import Checkout from '../views/Checkout'
import OrderSuccess from '../views/OrderSuccess'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Main',
    component: Main
  },
  {
    path: '/checkout/:total',
    name: 'Checkout',
    component: Checkout
  },
  {
    path: '/order',
    name: 'OrderSuccess',
    component: OrderSuccess
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
