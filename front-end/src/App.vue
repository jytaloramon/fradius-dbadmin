<template>
  <div class="w-screen h-screen flex flex-row font-sans">
    <router-view/>
  </div>

  <!--
  <div class="w-screen h-screen flex flex-row font-sans">
    <the-side-bar class="w-2/12 min-w-[175px]" />

    <main class="w-10/12 px-6 py-3 bg-gray-100">
      <top-bar class="mt-3 mb-6" />
      
      <router-view />
    </main>
  </div>
  -->
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TheSideBar from '@/components/navigation/TheSideBar.vue';
import TopBar from './components/navigation/TopBar.vue';


export default defineComponent({
  name: 'App',
  components: { TheSideBar, TopBar }
});
</script>