import { createRouter, createWebHistory } from 'vue-router';

import UserView from '@/views/UserView.vue';
import UsersListView from '@/views/Users/UsersListView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/users', component: UserView, children: [{ path: '', component: UsersListView }] }
  ]
});

export default router;
