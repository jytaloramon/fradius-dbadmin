<template>
  <li :class="classType[itemDesc.key === itemStored.getActualItemSelected.key ? 1 : 0]">
    <RouterLink :to="itemDesc.path">
      <font-awesome-icon :icon="itemDesc.icon" />
      <span class="ml-2">{{ $t(`label.${itemDesc.key}`) }}</span>
    </RouterLink>
  </li>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import { type IItemSideBarComum } from '@/interfaces/ISidebarItem';


export default defineComponent({
  name: 'ItemSideBarComum',

  props: {
    itemDesc: {
      type: Object as PropType<IItemSideBarComum>,
      required: true
    }
  },

  data() {
    return {
      itemStored: menuItemStore(),
      classType: ['w-full flex justify-between py-2 px-3 mt-2', 'flex justify-between py-2 px-3 mt-2 text-pink-700 bg-gradient-to-r from-pink-50 border-l-2 border-pink-700']
    };
  }
});
</script>