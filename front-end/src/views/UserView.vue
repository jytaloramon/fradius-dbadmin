<template>
  <section class="h-full px-8 py-6 bg-slate-100">
    <top-bar :title="$t('label.users')" :menu-items="menuItems" />

    <div class="pt-6">
      <router-view />
    </div>
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
        { label: this.$t('label.view'), path: '' },
        { label: this.$t('label.add'), path: '/add' },
        { label: this.$t('label.merger'), path: '' }
      ]
    };
  },

  mounted() {
    menuItemStore().selectUsers();
  },
});
</script>