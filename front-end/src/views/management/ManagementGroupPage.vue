<template>
  <div class="flex md:h-[576px] py-4">
    <app-box :title="'Lista de Usuários'" class="w-1/2 md:h-[560px]">
      <div>
        <div class="inline-block">
          <span class="text-base">Lista de Usuários</span>
          <span class="ml-3">&lt;</span>
          <span class="ml-2">&gt;</span>
        </div>

        <div class="float-right">
          <button class="px-3 py-1 text-sm border rounded-md">
            <font-awesome-icon icon="fa-solid fa-rotate-left" size="sm" />
            atualizar
          </button>
        </div>
      </div>

      <div>
        <app-table class="mt-8" :pagination="{ actual: 1, max: 2 }">
          <template v-slot:head>
            <table-head-item-with-sort class="py-2 px-3">ID</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Name</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Nº Usuários</table-head-item-with-sort>
          </template>

          <template v-slot:body>
            <tr v-for="(group, idx) in tableData.getItems()" :key="idx"
              class="border-b cursor-pointer even:bg-white odd:bg-slate-100">
              <td class="p-3">#{{ group.id }}</td>
              <td class="p-3">@{{ group.name }}</td>
              <td class="p-3">{{ group.qtUsers }}</td>
            </tr>
          </template>
        </app-table>
      </div>
    </app-box>

    <app-box class="w-1/2 md:h-[560px] ml-6 overflow-y-auto overflow-x-auto">
      <div class="">
        <p class="font-semibold">Grupo: Grupo1</p>

        <p class="mt-1 text-sm">Habilite/Desabilite as permissões deste grupo</p>
      </div>

      <div class="text-sm">
        <div v-for="temp in 2" :key="temp" class="mt-4">
          <div class="pt-4 border-t border-dashed border-gray-300">
            <p class="inline font-semibold">Gerenciamento</p>
            <span class="mt-0.5 float-right"><checkbox-with-square-icon :is-checked-input="false" /></span>
          </div>

          <table class="mt-3 table table-auto mx-auto">
            <tbody>
              <tr v-for="idx in Math.ceil(perm.length / tableWrap)" :key="idx" class="">
                <td v-for="i in Math.min(perm.length - (idx - 1) * tableWrap, tableWrap)"
                  :key="(idx - 1) * tableWrap + (i - 1)" class="py-2 pr-6">

                  <span class="mt-0.5"><checkbox-with-square-icon :is-checked-input="false" /></span>
                  <span class="ml-3"> {{ perm[(idx - 1) * tableWrap + (i - 1)] }}</span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="mt-6">
          <button-with-loading
            :button-with-loading-desc="{ label: 'Salvar', icon: { icon: 'fa-solid fa-floppy-disk' } }" />
        </div>
      </div>
    </app-box>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import AppBox from '@/components/boxes/AppBox.vue';
import AppTable from '@/components/table/AppTable.vue';
import ButtonWithLoading from '@/components/buttons/ButtonWithLoading.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import Table from '@/models/Table';
import TableHeadItemWithSort from '@/components/table/TableHeadItemWithSort.vue';


interface IGroup {
  id: number;
  name: string;
  qtUsers: number;
}


export default defineComponent({
  name: 'ManagementGroupPage',

  components: { AppBox, AppTable, ButtonWithLoading, CheckboxWithSquareIcon, TableHeadItemWithSort },

  data() {
    return {
      menuStore: menuItemStore(),
      tableData: new Table<IGroup>(),
      tableWrap: 4,
      perm: [
        'Grupo 1',
        'Grupo 2',
        'Grupo 3',
        'Grupo 4',
        'Grupo 5',
        'Grupo 6',
        'Grupo 7'
      ]
    };
  },

  created() {
    this.menuStore.selectManagement(1);

    this.tableData.appendItems([
      { id: 1, name: 'Group 1', qtUsers: 15 },
      { id: 2, name: 'Group 2', qtUsers: 1 },
      { id: 3, name: 'Group 3', qtUsers: 50 },
    ]);
  }
});
</script>