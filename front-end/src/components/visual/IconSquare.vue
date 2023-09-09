<template>
  <app-square-icon :description="description" class="w-10 h-10 text-xs" />
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppSquareIcon, { type IAppIconSquare } from './AppIconSquare.vue';

export default defineComponent({
  name: 'IconSquare',

  props: {
    description: {
      type: Object as PropType<IAppIconSquare>,
      required: true
    }
  },

  components: { AppSquareIcon }
});
</script>