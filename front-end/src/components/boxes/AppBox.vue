<template>
  <div class="flex flex-col py-8 px-6 text-gray-700 bg-white rounded-md shadow-md">
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'AppBox'
});
</script>
