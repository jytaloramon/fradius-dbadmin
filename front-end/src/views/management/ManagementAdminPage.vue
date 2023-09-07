<template>
  <div class="flex md:h-[576px] py-4">
    <app-box :class="style.boxList">
      <div>
        <div class="inline-block">
          <span class="text-base">Lista de Usuários</span>
          <span class="ml-3">&lt;</span>
          <span class="ml-2">&gt;</span>
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
              <td class="py-3 px-3">
                <checkbox-with-square-icon class="mt-2" :is-checked-input="tableData.getCheckedIndexes().has(idx)" />
              </td>
              <td class="py-3 px-3">#{{ user.id }}</td>
              <td class="py-3 px-3">@{{ user.username }}</td>
              <td class="py-3 px-3">{{ user.email }}</td>
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

    <box-with-close-button v-if="form.isVisible" :title="'Administrador'"
      class="w-1/2 ml-6 md:h-full overflow-y-auto overflow-x-auto" @closeBox="formClose">
      <p class="pb-4 border-b border-dashed border-gray-300">Adicione um novo administrador</p>

      <form class="mt-4">
        <div class="pb-7 border-b">
          <p class="font-semibold">Conta</p>

          <input-text-with-left-icon
            :input-desc="{ label: 'Usuário', placeholder: 'ramon04', data: form.field.username, icon: 'fa-regular fa-user' }"
            @inputChanged="formFieldSetUsername" />

          <input-text-with-left-icon class="mt-3"
            :input-desc="{ label: 'E-mail', type: 'email', placeholder: 'mary@email.com', data: form.field.email, icon: 'fa-solid fa-envelope' }"
            @inputChanged="formFieldSetEmail" />

          <select-with-left-icon
            :select-desc="{ label: 'Grupo', icon: 'fa-solid fa-layer-group', data: ['Grupo 1', 'Grupo 2'] }"
            class="mt-3" />
        </div>

        <div class="mt-5">
          <p class="font-semibold">Entrada de Senha</p>

          <div class="mt-3">
            <input-radio :input-desc="{ label: 'Automático', group: 'password', value: 'auto', isChecked: true }"
              class="inline ml-1" @inputChanged="formSetPaswordMode" />

            <input-radio :input-desc="{ label: 'Manual', group: 'password', value: 'manual' }" class="inline ml-8"
              @inputChanged="formSetPaswordMode" />

            <div v-if="form.passwordMode === 'manual'" class="mt-2">
              <input-text-with-left-icon
                :input-desc="{ label: 'Senha', type: 'password', placeholder: '**** ****', data: form.field.password, icon: 'fa-solid fa-lock' }"
                @inputChanged="formFieldSetPassword" />

              <input-text-with-left-icon class="mt-3"
                :input-desc="{ label: 'Confirme', type: 'password', placeholder: '**** ****', data: form.field.passwordCheck, icon: 'fa-solid fa-lock' }"
                @inputChanged="formFieldSetPasswordCheck" />
            </div>
          </div>
        </div>

        <div class="mt-8">
          <button-with-icon-large class="w-full text-white bg-green-600 border-0 "
            :description="{ label: 'Salvar', icon: { icon: 'fa-solid fa-plus' }, isDisabled: false }" />
        </div>
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
import ButtonSwitch from '@/components/buttons/ButtonSwitch.vue';
import ButtonWithIconLarge from '@/components/buttons/ButtonWithIconLarge.vue';
import CheckboxWithLessIcon from '@/components/inputs/CheckboxWithLessIcon.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import InputRadio from '@/components/inputs/InputRadio.vue';
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue';
import Table from '@/models/Table';
import SelectWithLeftIcon from '@/components/inputs/SelectWithLeftIcon.vue';
import TableHeadItemWithSort from '@/components/table/TableHeadItemWithSort.vue';


interface IAdmin {
  id: number;
  username: string;
  email: string;
  group: string;
  lastAccess: string;
  isActive: boolean;
}


export default defineComponent({
  name: 'ManagementAdminPage',

  components: { AppBox, AppTable, ButtonSwitch, BoxWithCloseButton, ButtonWithIconLarge, CheckboxWithLessIcon, CheckboxWithSquareIcon, InputRadio, InputTextWithLeftIcon, SelectWithLeftIcon, TableHeadItemWithSort },

  data() {
    return {
      style: {
        boxList: ''
      },
      menuStore: menuItemStore(),
      tableData: new Table<IAdmin>(),
      form: {
        isVisible: false,
        passwordMode: '',
        field: {
          username: {
            value: '',
            error: '',
          },
          email: {
            value: '',
            error: '',
          },
          password: {
            value: '',
            error: '',
          },
          passwordCheck: {
            value: '',
            error: '',
          },
          groupId: 0,
        }
      }
    };
  },

  created() {
    this.menuStore.selectManagement(0);

    this.tableData.appendItems([
      { id: 1, username: 'ramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Mais de três meses', isActive: true },
      { id: 2, username: 'ron04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há dois meses', isActive: true },
      { id: 3, username: 'rn04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há um més', isActive: false },
      { id: 4, username: 'jamon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Há uma semana', isActive: true },
      { id: 5, username: 'ddsramon04', email: 'ramongoncalves76@gmail.com', group: 'admin', lastAccess: 'Hoje', isActive: false },
    ]);

    this.formOpen();
  },

  methods: {
    formClose(): void {
      this.form.isVisible = false;
      this.style.boxList = 'w-full md:h-full'
    },
    formOpen(admin?: IAdmin): void {
      if (!admin) this.formSetValues();

      this.form.isVisible = true;
      this.style.boxList = 'w-1/2 md:h-full'
    },
    formSetValues() {
      this.form.field.username.value = '';
      this.form.field.email.value = '';
      this.form.field.groupId = 0;
      this.form.field.password.value = '';
      this.form.field.passwordCheck.value = '';
    },
    formSetPaswordMode(mode: string): void {
      this.form.passwordMode = mode
    },
    formFieldSetUsername(username: string): void {
      this.form.field.username.value = username;
    },
    formFieldSetEmail(email: string): void {
      this.form.field.email.value = email;
    },
    formFieldSetPassword(password: string): void {
      this.form.field.password.value = password;
    },
    formFieldSetPasswordCheck(passwordCheck: string): void {
      this.form.field.passwordCheck.value = passwordCheck;
    }
  }
});
</script>
