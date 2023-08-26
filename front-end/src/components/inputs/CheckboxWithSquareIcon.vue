<template>
  <input type="checkbox" :checked="isCheckedInput"
    class="w-[14px] h-[14px] appearance-none bg-white rounded-[2px] border-[2px] border-gray-700 hover:cursor-pointer after:w-2 after:block checked:after:bg-gray-700 after:h-2 after:ml-[1px] after:mt-[1px]"
    @change="e => $emit('inputChanged', e.target?.checked)">
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'CheckboxWithSquareIcon',

  props: {
    isCheckedInput: Boolean
  }
});
</script>