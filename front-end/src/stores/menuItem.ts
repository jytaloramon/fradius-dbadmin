import IndexOutOfRangeException from '@/exceptions/IndexOutOfRangeException';
import { defineStore } from 'pinia';

interface IItemSelected {
  level: number;
  subLevel?: number;
}

const menuItem = {
  dashboard: { id: 0, subMenuQt: 0 },
  users: { id: 1, subMenuQt: 3 }
};

const changeItem = (
  itemSelectedStore: IItemSelected,
  newItem: 'dashboard' | 'users',
  newSubLevelIdx?: number
) => {
  if (newSubLevelIdx && (newSubLevelIdx < 0 || newSubLevelIdx > menuItem[newItem].subMenuQt))
    throw new IndexOutOfRangeException('Index Out of Range');

  itemSelectedStore.level = menuItem[newItem].id;
  itemSelectedStore.subLevel = newSubLevelIdx;
};

export const menuItemStore = defineStore('menu-item', {
  state() {
    return {
      itemSelected: { level: 0, subLevel: undefined }
    };
  },
  getters: {
    getActualItemSelected: (state) => state.itemSelected
  },
  actions: {
    selectDashboard() {
      changeItem(this.itemSelected, 'dashboard');
    },
    selectUsers(newIdx: number) {
      changeItem(this.itemSelected, 'users', newIdx);
    }
  }
});
