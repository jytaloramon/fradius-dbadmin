<template>
  <div class="p-6">
    <div>
      <span class="font-semibold text-xl text-gray-800">{{ title }}</span>
    </div>

    <div class="mt-1 font-semibold text-sm text-gray-700">
      <ul class="flex flex-row">
        <li v-for="(item, idx) in menuItems" :key="idx" :class="classMenuItem[idx == itemIdxSelected ? 1 : 0]">
          <a class="hover:cursor-pointer" @click="changeItemMenu(idx)">{{ item.label }}</a>
        </li>
      </ul>
      <div class="-m-[2px] border-b-2"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';

interface ITopBarItem {
  label: string;
  path: string;
}


export default defineComponent({
  name: 'TopBar',

  props: {
    title: String,

    menuItems: {
      type: Object as PropType<ITopBarItem[]>,
      required: true
    }
  },

  data() {
    return {
      classMenuItem: [
        'py-3 ml-2 px-2 first:pl-0 first:ml-0',
        'py-3 ml-2 px-2 border-b-[3px] first:pl-0 first:ml-0 border-gray-700'
      ],
      itemIdxSelected: 0
    };
  },

  methods: {
    changeItemMenu(newItemIdx: number) {
      if (newItemIdx === this.itemIdxSelected) return;

      this.$router.push(this.menuItems[newItemIdx].path);
      this.itemIdxSelected = newItemIdx;
    }
  }
});
</script>
