<template>
  <section>
    <top-bar :title="'Usuário'" :menu-items="menuItems" @item-clicked="(n) => { console.log(n) }" />

    <router-view />
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menuItem';
import TopBar from '@/components/TopBar.vue';

export default defineComponent({
  name: 'UserView',

  components: { TopBar },

  data() {
    return {
      menuItems: [
        { label: 'Visualizar', path: '', isSelected: false },
        { label: 'Adicionar', path: '/add', isSelected: false },
        { label: 'Mesclar', path: '', isSelected: true }
      ]
    };
  },

  mounted() {
    menuItemStore().selectUsers();
  },
});
</script>