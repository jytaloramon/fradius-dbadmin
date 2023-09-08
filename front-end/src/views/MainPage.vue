<template>
  <div class="flex flex-col md:flex-row w-full h-full">
    <the-side-bar />

    <div class="w-full h-full md:w-10/12 px-1 md:px-3 bg-gray-100">
      <the-top-bar />

      <router-view />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TheSideBar from '@/components/navigation/TheSideBar.vue';
import TheTopBar from '@/components/navigation/TheTopBar.vue';

export default defineComponent({
  name: 'MainPage',

  components: { TheSideBar, TheTopBar }
});
</script>