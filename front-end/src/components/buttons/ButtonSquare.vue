<template>
  <app-button-square :description="{
    label: { value: description.label, style: description.style.borderColor },
    design: { icon: description.icon, size: 'xs', style: `p-3 ${description.style.borderColor}` }
  }" />
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';
import AppButtonSquare from './AppButtonSquare.vue';


interface IButtonSquare {
  label: string;
  icon: string;
  style: {
    borderColor: string;
  }
}


export default defineComponent({
  name: 'ButtonSquare',

  props: {
    description: {
      type: Object as PropType<IButtonSquare>,
      required: true
    }
  },

  components: { AppButtonSquare }
});
</script>
