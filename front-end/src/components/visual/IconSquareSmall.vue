<template>
  <app-square-icon :description="description" class="w-7 h-7 text-xs" />
</template>
  
<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppSquareIcon, { type IAppIconSquare } from './AppIconSquare.vue';

export default defineComponent({
  name: 'IconSquareSmall',

  props: {
    description: {
      type: Object as PropType<IAppIconSquare>,
      required: true
    }
  },

  components: { AppSquareIcon }
});
</script>