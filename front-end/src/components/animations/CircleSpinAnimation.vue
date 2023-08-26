<template>
  <span class="w-6 h-6 ml-3 rounded-full border-2 border-b-transparent border-l-transparent animate-rotate"></span>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'CircleSpinAnimation'
});
</script>

<style scoped>
.animate-rotate {
  animation: identifier 1000ms;
  animation-timing-function: linear;
  animation-iteration-count: infinite;
}

@keyframes identifier {
  from {
    transform: rotate(0turn);
  }

  to {
    transform: rotate(1turn);
  }
}
</style>