<template>
  <section>
    <top-bar class="px-6" />
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TopBar from '@/components/navigation/TopBar.vue';

export default defineComponent({
  name: 'DashboardView',
  components: { TopBar }
});
</script>