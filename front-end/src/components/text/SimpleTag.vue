<template>
  <span class="py-0.5 px-2 rounded-md">{{ text }}</span>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'SimpleTag',

  props: {
    text: String
  },
});
</script>