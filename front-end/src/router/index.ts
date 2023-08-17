import { createRouter, createWebHistory } from 'vue-router';

import AccountPage from '@/views/account/AccountPage.vue';
import AccountSigninPage from '@/views/account/AccountSigninPage.vue';
import AccountRecoveryPage from '@/views/account/AccountRecoveryPage.vue';

import UserView from '@/views/users/UsersView.vue';
import UserAddView from '@/views/users/UserAddView.vue';
import UsersListView from '@/views/users/UsersListView.vue';
import ManagementPage from '@/views/management/ManagementPage.vue';
import ManagementAdminPage from '@/views/management/ManagementAdminPage.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/account',
      component: AccountPage,
      redirect: 'account/signin',
      children: [
        { path: 'signin', component: AccountSigninPage },
        { path: 'recovery', component: AccountRecoveryPage }
      ]
    },
    {
      path: '/management',
      component: ManagementPage,
      redirect: '',
      children: [{ path: 'admin', component: ManagementAdminPage }]
    },
    {
      path: '/users',
      component: UserView,
      children: [
        { path: '', component: UsersListView },
        { path: '/add', component: UserAddView }
      ]
    }
  ]
});

export default router;
