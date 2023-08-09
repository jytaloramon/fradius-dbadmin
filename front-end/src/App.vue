<template>
  <div class="w-screen h-screen font-sans flex flex-row">
    <the-menu-global class="w-2/12 min-w-[175px]" />
    <main class="w-10/12">
      <dashboard-view />
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TheMenuGlobal from '@/components/navigation/TheMenuGlobal.vue';
import DashboardView from '@/views/DashboardView.vue';

export default defineComponent({
  name: 'App',
  components: { TheMenuGlobal, DashboardView }
});
</script>