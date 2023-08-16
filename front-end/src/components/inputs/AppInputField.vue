<template>
  <div class="pt-4">
    <div class="p-3 rounded-lg border">
      <label class="absolute px-2 -mt-6 ml-0 text-xs bg-white">{{ fieldDesc.label }}</label>
      <slot></slot>
    </div>

    <div v-if="fieldDesc.error" class="mt-1 ml-5 text-xs text-red-600">
      <span><font-awesome-icon icon="fa-solid fa-triangle-exclamation" size="sm" /></span>
      <span class="ml-2">{{ fieldDesc.error }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';

interface IAppInputField {
  label: string;
  error?: string;
}

export default defineComponent({
  name: 'AppInputField',

  props: {
    fieldDesc: {
      type: Object as PropType<IAppInputField>,
      required: true
    }
  }
});
</script>
