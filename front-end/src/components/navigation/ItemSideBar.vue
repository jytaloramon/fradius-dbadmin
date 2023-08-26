<template>
  <item-side-bar-comum v-if="isItemComum()" :item-desc="(itemMenu as IItemSideBarComum)" />
  <item-side-bar-with-submenu v-else :item-desc="itemMenu" />
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import type { IItemSideBarComum, IItemSideBarWithSubmenu } from '@/interfaces/ISidebarItem'
import ItemSideBarComum from './ItemSideBarComum.vue';
import ItemSideBarWithSubmenu from './ItemSideBarWithSubmenu.vue';


export default defineComponent({
  name: 'ItemSideBar',

  components: { ItemSideBarComum, ItemSideBarWithSubmenu },

  props: {
    itemMenu: {
      type: Object as PropType<IItemSideBarComum | IItemSideBarWithSubmenu>,
      required: true
    }
  },

  methods: {
    isItemComum(): boolean {
      return !this.itemMenu.items;
    }
  }
});
</script>