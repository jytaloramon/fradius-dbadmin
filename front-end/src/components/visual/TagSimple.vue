<template>
  <span class="py-0.5 px-2 border rounded-md">{{ text }}</span>
</template>
  
<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'TagSimple',

  props: {
    text: {
      type: String,
      required: true
    }
  }
});
</script>
