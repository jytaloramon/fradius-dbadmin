<template>
  <div class="py-5">
    <div>
      <span class="font-semibold text-xl text-gray-800">{{ title }}</span>
    </div>

    <div class="mt-1 font-semibold text-sm text-gray-700">
      <ul class="flex flex-row">
        <li v-for="(item, idx) in menuItems" :key="idx" :class="classMenuItem[item.classIndex]">
          <a href="">{{ item.label }}</a>
        </li>
      </ul>
      <div class="-m-[2px] border-b-2"></div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
  name: 'TopBar',
  data() {
    return {
      title: 'Dashboard',
      classMenuItem: [
        'py-3 ml-2 px-2 first:pl-0 first:ml-0',
        'py-3 ml-2 px-2 border-b-[3px] first:pl-0 first:ml-0 border-gray-700'
      ],
      menuItems: [
        { label: 'Todas as Métricas', path: '', classIndex: 0 },
        { label: 'Gráficos', path: '', classIndex: 0 },
        { label: 'Email', path: '', classIndex: 1 },
      ],
    };
  },
});
</script>