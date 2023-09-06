<template>
  <button :disabled="description.isDisabled"
    class="py-2 px-4 text-green-600 border border-green-600 rounded-md hover:shadow-md">
    <slot></slot>
    <span>{{ description.label }}</span>
  </button>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


export interface IAppButton {
  label: string;
  isDisabled?: boolean
}


export default defineComponent({
  name: 'AppButton',

  props: {
    description: {
      type: Object as PropType<IAppButton>,
      required: true
    }
  }
});
</script>
