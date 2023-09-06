<template>
  <app-button-with-icon :description="description" class="text-base" />
</template>
  
<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppButtonWithIcon, { type IAppButtonWithIcon } from './AppButtonWithIcon.vue';


export default defineComponent({
  name: 'ButtonWithIconLarge',

  props: {
    description: {
      type: Object as PropType<IAppButtonWithIcon>,
      required: true
    }
  },

  components: { AppButtonWithIcon }
});
</script>