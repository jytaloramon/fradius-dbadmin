<template>
  <div class="w-screen h-screen flex flex-row font-sans">
    <the-side-bar class="w-2/12 min-w-[175px]" />
    
    <main class="w-10/12">
      <router-view />
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TheSideBar from '@/components/navigation/TheSideBar.vue';


export default defineComponent({
  name: 'App',
  components: { TheSideBar }
});
</script>