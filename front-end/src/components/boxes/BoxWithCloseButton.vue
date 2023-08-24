<template>
  <app-box>
    <div class="">
      <p class="float-left font-semibold">{{ title }}</p>

      <a class="float-right cursor-pointer" @click="$emit('closeBox')">
        <font-awesome-icon class="py-1 px-[6px] text-red-600 bg-red-200 rounded-full hover:bg-red-300"
          icon="fa-solid fa-xmark" />
      </a>
    </div>

    <slot></slot>
  </app-box>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppBox from './AppBox.vue';


export default defineComponent({
  name: 'BoxWithCloseButton',

  components: { AppBox },

  props: {
    title: {
      type: String,
      required: true
    }
  }
});
</script>