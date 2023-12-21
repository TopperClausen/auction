import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import { createPinia } from 'pinia'
import routes from './routes';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)

createApp(App)
    .use(router)
    .use(pinia)
    .mount('#app');
