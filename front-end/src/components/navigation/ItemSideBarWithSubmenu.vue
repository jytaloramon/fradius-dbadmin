<template>
  <li>
    <div :class="classType.item[itemDesc.key === itemStored.getActualItemSelected.key ? 1 : 0]">
      <a class="cursor-pointer">
        <span><font-awesome-icon :icon="itemDesc.icon" /></span>
        <span class="ml-3">{{ $t(`label.${itemDesc.key}`) }}</span>
      </a>

      <a class="cursor-pointer" @click="changeIsVisibleValue()">
        <font-awesome-icon icon="fa-solid fa-chevron-down" size="xs" :rotation="isVisible ? 0 : 180" />
      </a>
    </div>

    <div v-if="isVisible">
      <ul>
        <li v-for="(item, idx) in itemDesc.items" :key="idx"
          :class="classType.subItem[itemDesc.key === itemStored.getActualItemSelected.key && itemStored.getActualItemSelected.subLevel === idx ? 1 : 0]">
          <RouterLink :to="item.path">
            <span><font-awesome-icon :icon="item.icon" /></span>
            <span class="ml-3">{{ $t(`label.${item.key}`) }}</span>
          </RouterLink>
        </li>
      </ul>
    </div>
  </li>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import { type IItemSideBarWithSubmenu } from '@/interfaces/ISidebarItem';


export default defineComponent({
  name: 'ItemSideBarWithSubmenu',

  props: {
    itemDesc: {
      type: Object as PropType<IItemSideBarWithSubmenu>,
      required: true
    }
  },

  data() {
    return {
      itemStored: menuItemStore(),
      isVisible: false,
      classType: {
        item: ['flex justify-between py-2 px-3 mt-2', 'flex justify-between py-2 px-3 mt-2 text-pink-700 bg-gradient-to-r from-pink-50 border-l-2 border-pink-700'],
        subItem: ['py-2 px-3 border-l-2 border-pink-200', 'py-2 px-3 text-pink-500 border-l-2 border-pink-200']
      },
    };
  },

  methods: {
    changeIsVisibleValue(): void {
      this.isVisible = !this.isVisible;
    }
  }
});
</script>