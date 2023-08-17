<template>
  <aside class="h-full px-5 flex flex-col justify-between border-r bg-white">
    <div>
      <div class="pt-7 mb-9 font-semibold text-xl">
        <span class="ml-2 text-purple-600">FR</span>
        <span class="ml-1 text-pink-400">DbAdmin</span>
      </div>

      <div>
        <ul class="text-sm text-neutral-900">
          <item-side-bar v-for="item in menuItems" :key="item.key" :item-menu="item" />
        </ul>
      </div>
    </div>

    <div class="mb-5">
      <ul class="text-sm text-neutral-900">
        <li class="px-4 py-2 mt-2 rounded-md">
          <a href="" class="">
            <span><font-awesome-icon icon="fa-solid fa-arrow-up-from-bracket" rotation="90" /></span>
            <span class="ml-3">{{ $t('label.exit') }} (</span>
            <span class="text-xs">@jytaloramon</span>
            <span>)</span>
          </a>
        </li>
      </ul>
    </div>
  </aside>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { sideBarItemsValue } from '@/value-objects/sidebar-item-values';

import ItemSideBar from './ItemSideBar.vue';

export default defineComponent({
  name: 'TheSideBar',

  components: { ItemSideBar },

  data() {
    return {
      classTypeMenuItem: ['px-3 py-2 mt-2 rounded-md', 'px-3 py-2 mt-2 rounded-md bg-pink-100 text-pink-700'],
      menuItems: sideBarItemsValue
    }
  },
});
</script>
