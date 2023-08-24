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
            <th class="py-2 px-3">
              <checkbox-with-less-icon v-if="tableData.getCheckedIndexes().size < tableData.getItems().length"
                :is-checked-input="tableData.getCheckedIndexes().size > 0" class="mt-2" />

              <checkbox-with-square-icon v-else :is-checked-input="true" class="mt-2" />
            </th>

            <table-head-item-with-sort class="py-2 px-3">ID</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Usuário</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">E-mail</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Grupo</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Online</table-head-item-with-sort>
            <table-head-item-with-sort class="py-2 px-3">Status</table-head-item-with-sort>
          </template>

          <template v-slot:body>
            <tr v-for="(user, idx) in tableData.getItems()" :key="idx" class="border-b even:bg-white odd:bg-slate-100">
              <td class="p-3">
                <checkbox-with-square-icon class="mt-2" :is-checked-input="tableData.getCheckedIndexes().has(idx)" />
              </td>
              <td class="p-3">#{{ user.id }}</td>
              <td class="p-3">
                <div class="flex">
                  <img class="w-6 h-6 rounded-full" src="/Ramon-Editia.jpg" alt="profile picture">
                  <p class="ml-2">@{{ user.username }}</p>
                </div>
              </td>
              <td class="p-3">{{ user.email }}</td>
              <td class="p-3">{{ user.group }}</td>
              <td class="p-3">
                <span class="bg-amber-200 font-semibold text-amber-800 text-xs py-1 px-2 rounded-md">
                  {{ user.lastAccess }}
                </span>
              </td>
              <td class="p-3">
                <span v-if="user.isActive"
                  class="bg-green-200 font-semibold text-green-800 text-xs py-1 px-2 rounded-md">Ativo</span>
                <span v-else class="bg-red-200 font-semibold text-red-800 text-xs py-1 px-2 rounded-md">Inativo</span>
              </td>
            </tr>
          </template>
        </app-table>
      </div>
    </app-box>

    <box-with-close-button v-if="isFormVisible" :title="'Administrador'"
      class="w-1/2 ml-6 md:h-[560px] overflow-y-auto overflow-x-auto" @closeBox="() => isFormVisible = false">
      <p class="text-sm pb-5 border-b border-gray-50">Adicione um novo administrador</p>

      <form class="mt-4">
        <div class="pb-7 border-b">
          <p class="font-semibold">Perfil</p>

          <input-text-with-left-icon
            :input-desc="{ label: 'Usuário', placeholder: 'ramon04', data: { value: '' }, icon: 'fa-regular fa-user' }"></input-text-with-left-icon>

          <input-text-with-left-icon class="mt-3"
            :input-desc="{ label: 'E-mail', type: 'email', placeholder: 'mary@email.com', data: { value: '' }, icon: 'fa-solid fa-envelope' }"></input-text-with-left-icon>

          <select-with-left-icon
            :select-desc="{ label: 'Grupo', icon: 'fa-solid fa-layer-group', data: ['Grupo 1', 'Grupo 2'] }"
            class="mt-3" />
        </div>

        <div class="mt-5">
          <p class="font-semibold">Entrada de Senha</p>

          <div class="mt-3">
            <input-radio :input-desc="{ label: 'Automático', group: 'password', value: 'auto', isChecked: true }"
              class="inline ml-1" @inputChanged="inputRadioChanged" />

            <input-radio :input-desc="{ label: 'Manual', group: 'password', value: 'manual' }" class="inline ml-8"
              @inputChanged="inputRadioChanged" />

            <div v-if="actualPasswordChecked === 'manual'" class="mt-2">
              <input-text-with-left-icon
                :input-desc="{ label: 'Senha', type: 'password', placeholder: '**** ****', data: { value: '' }, icon: 'fa-solid fa-lock' }"></input-text-with-left-icon>

              <input-text-with-left-icon class="mt-3"
                :input-desc="{ label: 'Confirme', type: 'password', placeholder: '**** ****', data: { value: '' }, icon: 'fa-solid fa-lock' }"></input-text-with-left-icon>
            </div>
          </div>
        </div>

        <button-with-loading class="mt-8"
          :button-with-loading-desc="{ label: 'Criar', icon: { icon: 'fa-solid fa-plus' } }" />
      </form>
    </box-with-close-button>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import AppBox from '@/components/boxes/AppBox.vue';
import AppTable from '@/components/table/AppTable.vue';
import BoxWithCloseButton from '@/components/boxes/BoxWithCloseButton.vue';
import ButtonWithLoading from '@/components/buttons/ButtonWithLoading.vue';
import CheckboxWithLessIcon from '@/components/inputs/CheckboxWithLessIcon.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import InputRadio from '@/components/inputs/InputRadio.vue';
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue';
import Table from '@/models/Table';
import SelectWithLeftIcon from '@/components/inputs/SelectWithLeftIcon.vue';
import TableHeadItemWithSort from '@/components/table/TableHeadItemWithSort.vue';



interface IUser {
  id: number;
  username: string;
  email: string;
  group: string;
  lastAccess: string;
  isActive: boolean;
}

export default defineComponent({
  name: 'ManagementAdminPage',

  components: { AppBox, AppTable, BoxWithCloseButton, ButtonWithLoading, CheckboxWithLessIcon, CheckboxWithSquareIcon, InputRadio, InputTextWithLeftIcon, SelectWithLeftIcon, TableHeadItemWithSort },

  data() {
    return {
      menuStore: menuItemStore(),
      tableData: new Table<IUser>(),
      actualPasswordChecked: 'auto',
      isFormVisible: true
    };
  },

  created() {
    this.menuStore.selectManagement(0);

    this.tableData.appendItems([
      { id: 1, username: 'ramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: '04/02/2023 às 15:35', isActive: true },
      { id: 2, username: 'ron04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: '04/02/2023 às 15:35', isActive: true },
      { id: 3, username: 'rn04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: '04/02/2023 às 15:35', isActive: false },
      { id: 4, username: 'jamon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: '04/02/2023 às 15:35', isActive: true },
      { id: 5, username: 'ddsramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: '04/02/2023 às 15:35', isActive: false },
    ]);
  },

  methods: {
    inputRadioChanged(value: string): void {
      if (value === this.actualPasswordChecked) return;

      this.actualPasswordChecked = value;
    }
  }
});
</script>
