<template>
  <section>
    <top-bar :title="'Dashboard'" :menu-items="menuItems" @item-clicked="(n) => { console.log(n) }" />
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TopBar from '@/components/navigation/TopBar.vue';

export default defineComponent({
  name: 'DashboardView',

  components: { TopBar },

  data() {
    return {
      menuItems: [
        { label: 'Todas as Métricas', path: '', isSelected: false },
        { label: 'Gráficos', path: '', isSelected: true },
        { label: 'Email', path: '', isSelected: false }
      ]
    };
  }
});
</script>