import type { IItemSideBarComum, IItemSideBarWithSubmenu } from '@/interfaces/ISidebarItem';

const sideBarItemsValue: { [key: string]: IItemSideBarComum | IItemSideBarWithSubmenu } = {
  dashboard: { key: 'dashboard', icon: 'fa-solid fa-chart-line', path: '/' },
  users: {
    key: 'users',
    icon: 'fa-regular fa-user',
    items: [
      { key: 'list', icon: 'fa-solid fa-list', path: '/user' },
      { key: 'add', icon: 'fa-solid fa-user-plus', path: '/user/add' }
    ]
  },
  nas: { key: 'nas', icon: 'fa-solid fa-network-wired', path: '' },
  management: {
    key: 'management',
    icon: 'fa-solid fa-sliders',
    items: [
      { key: 'admin', icon: 'fa-solid fa-user-astronaut', path: '/management/admin' },
      { key: 'groups', icon: 'fa-solid fa-layer-group', path: '/management/group' }
    ]
  }
};

export { sideBarItemsValue };
