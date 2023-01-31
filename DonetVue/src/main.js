import { createApp } from 'vue'

import App from './App.vue'

import '@/axios/axios.js'
import './assets/main.css'
import router from './router/index'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import axios from 'axios'

import VueAxios from 'vue-axios'

const app = createApp(App)

axios.defaults.baseURL='https://localhost:7085/'
app.use(router)
app.use(ElementPlus)

app.use(VueAxios, axios)
//然而只是这样全局并不能用;
app.provide('axios', app.config.globalProperties.axios) 
//这句不写, 组件里无法注入全局axios, 也就无法使用
app.mount('#app')
