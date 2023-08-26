<template>
  <div class="pt-2">
    <label class="relative z-30 px-2 ml-2.5 text-xs bg-white">{{ fieldDesc.label }}</label>

    <div class="relative z-10 p-3 rounded-lg border -mt-2">
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
