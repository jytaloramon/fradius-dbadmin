import { defineStore } from 'pinia';
import { sideBarItemsValue } from '@/value-objects/sidebar-item-values';
import type { IItemSideBarWithSubmenu } from '@/interfaces/ISidebarItem';
import IndexOutOfRangeException from '@/exceptions/IndexOutOfRangeException';
import KeyNotFound from '@/exceptions/KeyNotFound';

interface IItemSelected {
  key: string;
  subLevel?: number;
}

const changeItem = (
  itemSelectedStore: IItemSelected,
  key: 'dashboard' | 'users' | 'nas' | 'management',
  newSubLevelIdx: number = 0
) => {
  const newItem = sideBarItemsValue[key];
  if (!newItem) throw new KeyNotFound(`Key "${key}" not found.`);

  itemSelectedStore.key = newItem.key;

  if (!newItem.items) {
    itemSelectedStore.subLevel = undefined;

    return;
  }

  const newItemWithSub = newItem as IItemSideBarWithSubmenu;

  if (newSubLevelIdx >= newItemWithSub.items!.length)
    throw new IndexOutOfRangeException('Index Out of Range');

  itemSelectedStore.subLevel = newSubLevelIdx;
};

export const menuItemStore = defineStore('menu-item', {
  state() {
    return {
      itemSelected: { key: '', subLevel: undefined }
    };
  },
  getters: {
    getActualItemSelected: (state) => state.itemSelected
  },
  actions: {
    selectDashboard() {
      changeItem(this.itemSelected, 'dashboard');
    },
    selectUsers(SubLeverIdx: number) {
      changeItem(this.itemSelected, 'users', SubLeverIdx);
    },
    selectManagement(SubLeverIdx: number) {
      changeItem(this.itemSelected, 'management', SubLeverIdx);
    }
  }
});
