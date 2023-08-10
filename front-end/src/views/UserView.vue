<template>
  <section>
    <top-bar :title="'Usuário'" :menu-items="menuItems" @item-clicked="(n) => { console.log(n) }" />
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TopBar from '@/components/navigation/TopBar.vue';

export default defineComponent({
  name: 'UserView',

  components: { TopBar },

  data() {
    return {
      menuItems: [
        { label: 'Visualizar', path: '', isSelected: false },
        { label: 'Criar', path: '', isSelected: false },
        { label: 'Mesclar', path: '', isSelected: true }
      ]
    };
  }
});
</script>