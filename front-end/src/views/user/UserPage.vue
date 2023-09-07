<template>
  <div class="flex w-full h-full">
    <the-side-bar class="w-2/12" />

    <div class="w-10/12 px-4 bg-gray-100">
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
  name: 'UserPage',

  components: { TheSideBar, TheTopBar }
});
</script>