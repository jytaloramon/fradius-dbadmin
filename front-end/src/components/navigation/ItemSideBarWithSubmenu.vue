<template>
  <li>
    <div :class="itemClassType[itemDesc.id === itemStored.getActualItemSelected.level ? 1 : 0]">
      <span>
        <a class="cursor-pointer">
          <span><font-awesome-icon :icon="itemDesc.icon" /></span>
          <span class="ml-2">{{ itemDesc.label }}</span>
        </a>
      </span>

      <span class="float-right">
        <a class="cursor-pointer" @click="changeIsVisibleValue()">
          <font-awesome-icon icon="fa-solid fa-chevron-down" size="xs" :rotation="isVisible ? 0 : 180" />
        </a>
      </span>
    </div>

    <div v-if="isVisible" class="ml-4 py-2">
      <ul class="border-l-2 border-b-2 rounded-bl-lg pb-4 border-pink-200">
        <li v-for="(item, idx) in itemDesc.items" :key="idx"
          :class="subitemClassType[itemStored.getActualItemSelected.subLevel === idx ? 1 : 0]">
          <RouterLink :to="item.path">
            <span><font-awesome-icon :icon="itemDesc.icon" /></span>
            <span class="ml-2">{{ item.label }}</span>
          </RouterLink>
        </li>
      </ul>

      <p class="ml-2 -mt-5 p-3 bg-white text-xs">Ocultar</p>
    </div>
  </li>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import { menuItemStore } from '@/stores/menuItem';

import { type IItemSideBarWithSubmenu } from './interfaces/itemSideBar';


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
      itemClassType: ['px-3 py-2 mt-2 rounded-md', 'px-3 py-2 mt-2 rounded-md bg-pink-100 text-pink-700'],
      subitemClassType: ['-ml-[2px] mt-1 px-5 py-1 border-l-2 border-pink-200', '-ml-[2px] mt-1 px-5 py-1 border-l-2 border-pink-400'],
    };
  },

  methods: {
    changeIsVisibleValue(): void {
      this.isVisible = !this.isVisible;
    }
  }
});
</script>