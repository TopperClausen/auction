import Register from './views/Register.vue';
import Dashboard from './views/Dashboard.vue';
import Login from './views/Login.vue';
import NewCar from './views/NewCar.vue';

export default [
    { path: '/register', name: 'Register', component: Register },
    { path: '/Login', name: 'Login', component: Login },
    { path: '/dashboard', name: 'Dashboard', component: Dashboard },

    { path: '/cars/new', name: 'NewCar', component: NewCar }
]