import type { IItemSideBarComum, IItemSideBarWithSubmenu } from '@/interfaces/ISidebarItem';

const sidemenuItemsValue: { [key: string]: IItemSideBarComum | IItemSideBarWithSubmenu } = {
  dashboard: { key: 'dashboard', icon: 'fa-solid fa-chart-line', path: '/' },
  users: {
    key: 'users',
    icon: 'fa-regular fa-user',
    items: [
      { key: 'list', icon: 'fa-solid fa-list', path: '/users' },
    ]
  },
  nas: { key: 'nas', icon: 'fa-solid fa-network-wired', path: '' }
};

export { sidemenuItemsValue };
