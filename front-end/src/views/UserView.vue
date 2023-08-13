<template>
  <section class="">


    <div class="">
      <router-view />
    </div>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'UserView',

  components: {},

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
  },
});
</script>