<template>
  <li :class="classType[itemDesc.key === itemStored.getActualItemSelected.key ? 1 : 0]">
    <RouterLink :to="itemDesc.path">
      <span><font-awesome-icon :icon="itemDesc.icon" /></span>
      <span class="ml-2">{{ $t(`label.${itemDesc.key}`) }}</span>
    </RouterLink>
  </li>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import { menuItemStore } from '@/stores/menuItem';
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
      classType: ['px-3 py-2 mt-2 rounded-md', 'px-3 py-2 mt-2 rounded-md bg-pink-100 text-pink-700'],
    };
  }
});
</script>