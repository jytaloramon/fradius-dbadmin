<template>
  <div class="flex justify-between">
    <div class="mt-1">
      <p class="font-semibold text-xl">Dashboard</p>
    </div>

    <div class="flex">
      <div
        class="after:content-[''] after:w-1 after:h-1 after:ml-8 after:-mt-8 after:block after:bg-red-600 after:rounded-full">
        <font-awesome-icon class="ml-2 mt-1 p-2 border rounded-md" icon="fa-regular fa-bell" color="rgb(220,38,38)" />
      </div>

      <div class="ml-2 p-1 ">
        <img class="w-8 h-8 rounded-md" src="../../../public/Ramon-Editia.jpg" alt="profile picture">
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'TopBar',


  data() {
    return {

    };
  },

  methods: {

  }
});
</script>
