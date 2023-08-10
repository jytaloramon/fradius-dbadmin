import { createApp } from 'vue';
import { createPinia } from 'pinia';

import { i18nModule } from './modules/i18n';
import router from './router';

import App from './App.vue';

import './assets/main.css';
import FontAwesomeLibrary from '@/modules/FontAwesomeLibrary';

const app = createApp(App);

app.use(createPinia());
app.use(i18nModule);
app.use(router);
app.component('font-awesome-icon', FontAwesomeLibrary);

app.mount('#app');
