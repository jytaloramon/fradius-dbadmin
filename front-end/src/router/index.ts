import { createRouter, createWebHistory } from 'vue-router';

import AccountPage from '@/views/account/AccountPage.vue';
import AccountSigninPage from '@/views/account/AccountSigninPage.vue';
import AccountRecoveryPage from '@/views/account/AccountRecoveryPage.vue';


import MainPage from '@/views/MainPage.vue';
import UserPage from '@/views/user/UserPage.vue';
import UserAddPage from '@/views/user/UserAddPage.vue';
import UserListPage from '@/views/user/UserListPage.vue';
import ManagementPage from '@/views/management/ManagementPage.vue';
import ManagementAdminPage from '@/views/management/ManagementAdminPage.vue';
import ManagementGroupPage from '@/views/management/ManagementGroupPage.vue';

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
      path: '/',
      component: MainPage,
      redirect: '',
      children: [
        {
          path: '/management',
          component: ManagementPage,
          redirect: '',
          children: [
            { path: 'admin', component: ManagementAdminPage },
            { path: 'group', component: ManagementGroupPage }
          ]
        },
        {
          path: '/user',
          component: UserPage,
          children: [
            { path: '', component: UserListPage },
            { path: 'add', component: UserAddPage }
          ]
        }
      ]
    },
  ]
});

export default router;
