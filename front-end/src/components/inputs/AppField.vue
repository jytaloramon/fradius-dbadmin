<template>
  <div class="relative w-full h-11 border rounded-lg py-2 px-3 text-xs text-gray-700">
    <label class="-top-2.5 z-30 absolute px-2 text-xs bg-white">{{ description.label }}</label>

    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


export interface IAppField {
  label: string;
}


export default defineComponent({
  name: 'AppField',

  props: {
    description: {
      type: Object as PropType<IAppField>,
      required: true
    }
  }
});
</script>