<template>
  <aside class="w-full h-max md:w-56 sm:h-full mx-1 md:mx-0 px-5 pb-2 bg-white">
    <div class="flex h-14 mt-7 ml-3 text-green-600 font-semibold">
      <font-awesome-icon class="mt-1 text-3xl" icon="fa-solid fa-database" />

      <div class="mt-1 ml-2 text-xs">
        <div>FreeRadius</div>
        <div>Admin</div>
      </div>
    </div>

    <div class="">
      <ul class="text-sm text-neutral-900">
        <item-side-bar v-for="item in menuItems" :key="item.key" :item-menu="item" />
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
      menuItems: sideBarItemsValue
    }
  },
});
</script>
