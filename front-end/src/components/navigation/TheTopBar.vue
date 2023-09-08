<template>
  <div class="flex justify-between w-full h-16 px-5 mt-3 bg-white rounded-md text-sm">
    <div class="hidden md:block my-auto">
      <span class="p-1 font-semibold text-slate-50 bg-green-600 rounded-md shadow-md">FR</span>
      <span class="ml-2 font-semibold">{{ $t(`label.${menuStore.getActualItemSelected.key}`) }}</span>
    </div>

    <div class="flex justify-between md:justify-end w-full my-auto">
      <div class="flex">
        <p class="my-auto">Light Mode</p>
        <a
          class="w-9 h-5 py-[2px] px-1 my-auto ml-2 bg-gray-400 rounded-full cursor-pointer before:w-4 before:h-4 before: before:bg-white before:rounded-full before:float-left"></a>
      </div>

      <div class="flex ml-5 p-1">
        <div>
          <p class="font-semibold">ytalo.almeida</p>
          <p class="mt-[2px] text-xs">Administrado</p>
        </div>

        <img class="my-auto ml-4 w-8 h-8 rounded-full" src="/Ramon-Editia.jpg" alt="profile picture">
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';


export default defineComponent({
  name: 'TheTopBar',

  data() {
    return {
      menuStore: menuItemStore()
    };
  }
});
</script>
