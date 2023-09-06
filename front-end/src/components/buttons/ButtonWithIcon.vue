<template>
  <app-button-with-icon :description="description" class="text-sm" />
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppButtonWithIcon, { type IAppButtonWithIcon } from './AppButtonWithIcon.vue';


export default defineComponent({
  name: 'ButtonWithIcon',

  props: {
    description: {
      type: Object as PropType<IAppButtonWithIcon>,
      required: true
    }
  },

  components: { AppButtonWithIcon }
});
</script>