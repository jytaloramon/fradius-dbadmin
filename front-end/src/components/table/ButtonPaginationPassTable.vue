<template>
  <button @click="$emit('buttonClick', '')" class="px-4 py-1 border-gray-400 disabled:text-gray-600" :disabled="buttonDesc?.disable">
    {{ buttonDesc?.label }}
  </button>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';

interface IButtonPagination {
  label: string;
  disable?: boolean;
}

export default defineComponent({
  name: 'ButtonPaginationPassTable',

  props: {
    buttonDesc: {
      type: Object as PropType<IButtonPagination>,
      requerid: true,
    }
  }
});
</script>