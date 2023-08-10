<template>
  <aside class="h-full flex flex-col justify-between bg-slate-50">
    <div>
      <div class="px-6 pt-6 font-semibold text-xl">
        <span class="text-purple-600">FR</span>
        <span class="ml-1 text-pink-400">DbAdmin</span>
      </div>

      <div>
        <ul class="mt-6 text-neutral-900">
          <li v-for="(item, idx) in menuItems" :key="idx"
            :class="classTypeMenuItem[idx === itemStored.getActualItemSelected ? 1 : 0]">
            <RouterLink :to="item.path">
              <span><font-awesome-icon :icon="item.icon" /></span>
              <span class="ml-4">{{ item.label }}</span>
            </RouterLink>
          </li>
        </ul>
      </div>
    </div>

    <div>
      <ul class="mb-5 text-neutral-900">
        <li class="h-10 my-1 pt-2 px-6">
          <a href="" class="">
            <span><font-awesome-icon icon="fa-solid fa-arrow-up-from-bracket" rotation="90" /></span>
            <span class="ml-3">{{ $t('label.exit') }} (</span>
            <span class="text-xs">@jytaloramon</span>
            <span>)</span>
          </a>
        </li>
      </ul>
    </div>
  </aside>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menuItem';

export default defineComponent({
  name: 'TheSideBar',

  data() {
    return {
      classTypeMenuItem: ['h-10 my-1 pt-2 px-6', 'h-10 my-1 pt-2 px-6 bg-violet-200 after:content-[\'\'] after:block after:float-right after:-mt-2 after:-mr-6 after:w-1 after:h-10 after:bg-purple-400'],
      itemStored: menuItemStore(),
      menuItems: [
        {
          label: this.$t('label.dashboard'),
          icon: 'fa-solid fa-chart-line',
          path: '/',
        },
        {
          label: this.$t('label.users'),
          icon: 'fa-regular fa-user',
          path: '/users',
        }
      ]
    }
  },
});
</script>
