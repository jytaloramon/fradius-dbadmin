<template>
  <section>
    <top-bar :title="title" :menu-items="menuItems" />
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menuItem';
import TopBar from '@/components/TopBar.vue';

export default defineComponent({
  name: 'DashboardView',

  components: { TopBar },

  data() {
    return {
      title: this.$t('label.dashboard'),
      menuItems: [
        { label: 'Todas as Métricas', path: '' },
        { label: 'Gráficos', path: '' },
        { label: 'Email', path: '' }
      ]
    };
  },

  mounted() {
    menuItemStore().selectDashboard();
  },
});
</script>