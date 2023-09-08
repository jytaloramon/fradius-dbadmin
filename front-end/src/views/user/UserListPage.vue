<template>
  <div class="flex flex-col md:h-[600px] py-4">
    <app-box class="w-full">
      <ul class="grid grid-cols-2 lg:grid-cols-4">
        <li v-for="(item, idx) in geralInfo" :key="idx" class="flex my-4 text-sm text-pink-500">
          <div class="flex flex-col justify-center w-12 h-12 text-white bg-pink-500 rounded-md">
            <font-awesome-icon :icon="item.icon" />
          </div>
          <div class="mt-1 ml-2.5">
            <div>{{ item.title }}</div>
            <div class="font-semibold">{{ item.subTitle }}</div>
          </div>
        </li>
      </ul>
    </app-box>

    <app-box class="w-full mt-4 md:h-[410px]">
      <div>
        <div class="inline-block">
          <span class="text-base">Lista de Usuários</span>
          <span class="ml-3">&lt;</span>
          <span class="ml-2">&gt;</span>
        </div>
      </div>

      <div class="mt-8 overflow-y-auto overflow-x-auto">
        <app-table :pagination="{ actual: 1, max: 2 }">
          <template v-slot:head>
            <th class="py-2 px-3">
              <checkbox-with-less-icon v-if="tableData.getCheckedIndexes().size < tableData.getItems().length"
                :is-checked-input="tableData.getCheckedIndexes().size > 0" class="mt-2" />

              <checkbox-with-square-icon v-else :is-checked-input="true" class="mt-2" />
            </th>

            <table-head-item-with-sort class="py-2 px-3">Usuário</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Grupo Principal</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Online</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Status</table-head-item-with-sort>
          </template>

          <template v-slot:body>
            <tr v-for="(user, idx) in tableData.getItems()" :key="idx" class="border-b even:bg-white odd:bg-slate-100">
              <td class="py-3 px-3">
                <checkbox-with-square-icon class="mt-2" :is-checked-input="tableData.getCheckedIndexes().has(idx)" />
              </td>
              <td class="py-3 px-3">@{{ user.username }}</td>
              <td class="py-3 px-3">{{ user.group }}</td>
              <td class="py-3 px-3">
                <div class="w-max">
                  <span class="bg-blue-200 font-semibold text-blue-600 text-xs py-1 px-2 rounded-md">
                    {{ user.lastAccess }}
                  </span>
                </div>
              </td>
              <td class="py-2 px-3 relative">
                <button-switch class="top-4 bg-green-600 before:float-right" />
              </td>
            </tr>
          </template>
        </app-table>
      </div>
    </app-box>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import AppBox from '@/components/boxes/AppBox.vue';
import Table from '@/models/Table';
import AppTable from '@/components/table/AppTable.vue';
import TableHeadItemWithSort from '@/components/table/TableHeadItemWithSort.vue';
import CheckboxWithLessIcon from '@/components/inputs/CheckboxWithLessIcon.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import ButtonSwitch from '@/components/buttons/ButtonSwitch.vue';


interface IAdmin {
  id: number;
  username: string;
  email: string;
  group: string;
  lastAccess: string;
  isActive: boolean;
}


export default defineComponent({
  name: 'UseListPage',

  components: { AppBox, AppTable, ButtonSwitch, CheckboxWithLessIcon, CheckboxWithSquareIcon, TableHeadItemWithSort },

  data() {
    return {
      menuStore: menuItemStore(),
      geralInfo: [
        { title: 'Total de Usuário', subTitle: '10k', icon: 'fa-solid fa-globe' },
        { title: 'Total de Ativos', subTitle: '1k', icon: 'fa-solid fa-heart-pulse' },
        { title: 'Usuários sem Registro', subTitle: '500', icon: 'fa-solid fa-skull' },
        { title: 'Usuários sem E-mail', subTitle: '7.5k', icon: 'fa-solid fa-heart-pulse' }
      ],
      tableData: new Table<IAdmin>(),
    };
  },

  created() {
    this.menuStore.selectUsers(0);

    this.tableData.appendItems([
      { id: 1, username: 'ramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Mais de três meses', isActive: true },
      { id: 2, username: 'ron04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há dois meses', isActive: true },
      { id: 3, username: 'rn04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há um més', isActive: false },
      { id: 4, username: 'jamon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há uma semana', isActive: true },
      { id: 5, username: 'ddsramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Hoje', isActive: false },
      { id: 1, username: 'ramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Mais de três meses', isActive: true },
      { id: 2, username: 'ron04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há dois meses', isActive: true },
      { id: 3, username: 'rn04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há um més', isActive: false },
      { id: 4, username: 'jamon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há uma semana', isActive: true },
      { id: 5, username: 'ddsramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Hoje', isActive: false },
    ]);

  },
});
</script>
