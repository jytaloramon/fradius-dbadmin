<template>
  <div>
    <input type="radio" :name="inputDesc.group" :value="inputDesc.value" :checked="inputDesc.isChecked" class="relative appearance-none w-4 h-4 border-2 border-green-600 rounded-full checked:before:bg-green-600 cursor-pointer
    before:absolute before:w-2 before:h-2 before:mt-0.5 before:ml-0.5 before:rounded-full"
      @change="e => $emit('inputChanged', e.target?.value)">

    <label class="ml-2">{{ inputDesc.label }}</label>
  </div>
</template>

<script lang="ts">
import { defineComponent, type PropType } from 'vue';


interface IInputRadio {
  label: string;
  value: string;
  group: string;
  isChecked?: boolean
}


export default defineComponent({
  name: 'InputRadio',

  props: {
    inputDesc: {
      type: Object as PropType<IInputRadio>,
      required: true
    }
  }
});
</script>