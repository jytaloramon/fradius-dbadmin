import { defineStore } from 'pinia';

export const menuItemStore = defineStore('menu-item', {
  state() {
    return {
      itemSelected: 0
    };
  },
  getters: {
    getActualItemSelected: (state) => state.itemSelected
  },
  actions: {
    selectDashboard() {
      this.itemSelected = 0;
    },
    selectUsers() {
      this.itemSelected = 1;
    }
  }
});
