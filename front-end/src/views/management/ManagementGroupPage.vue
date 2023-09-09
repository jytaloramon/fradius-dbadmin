<template>
  <div class="flex flex-col md:flex-row md:h-[600px] py-4">
    <app-box class="w-full h-80 md:h-full md:w-1/2">
      <div class="flex justify-between">
        <span class="text-base">{{ $t('label.groups') }}</span>

        <a class="text-green-600 cursor-pointer" @click="setGroupToEdit()">
          <icon-square-small class="border-green-600" :description="{ icon: 'fa-solid fa-plus' }" />
        </a>
      </div>

      <div class="mt-8 overflow-y-auto overflow-x-auto">
        <table-simple>
          <template v-slot:head>
            <table-h-item-with-sort :description="{ isVisible: true }" class="py-2 px-3">ID</table-h-item-with-sort>
            <table-h-item-with-sort :description="{ isVisible: true }" class="py-2 px-3">Name</table-h-item-with-sort>
            <table-h-item-with-sort :description="{ isVisible: true }" class="py-2 px-3">
              Nº Usuários</table-h-item-with-sort>
          </template>

          <template v-slot:body>
            <tr v-for="(group, idx) in tableData.getItems()" :key="idx"
              class="border-b cursor-pointer even:bg-white odd:bg-slate-100" @click="setGroupToEdit(idx)">
              <td class="p-3">#{{ group.id }}</td>
              <td class="p-3">@{{ group.name }}</td>
              <td class="p-3">{{ group.qtUsers }}</td>
            </tr>
          </template>
        </table-simple>
      </div>
    </app-box>

    <app-box
      class="flex flex-col justify-between w-full md:w-1/2 md:h-full mt-6 md:mt-0 md:ml-6 overflow-y-auto overflow-x-auto">

      <div class="text-sm">
        <form>
          <div class="mb-3">
            <div class="font-semibold">
              <label>{{ $t('label.group') }}: </label>
              <input class="ml-1 border-b border-green-600 focus:outline-none" type="text"
                v-model="form.field.name.value">

              <tag-simple v-if="!oldGroup.data" text="Novo" class="float-right text-xs text-slate-50 bg-green-600" />
              <tag-simple v-else-if="openGroupHasNameChanged" :text="`Alterado: ${oldGroup.data.name}`"
                class="float-right text-xs text-slate-50 bg-red-500" />
            </div>

            <div class="mt-2 text-sm">{{ $t('message.groupSettingSubTitle') }}</div>
          </div>

          <div v-for="(ruleGroup, idx) in ruleGroups" :key="idx" class="pt-4 border-t border-dashed border-gray-300">
            <div class="">
              <span class="font-semibold">{{ $t(`label.${ruleGroup.name}`) }}</span>
              <span class="mt-0.5 float-right">
                <checkbox-with-square-icon :is-checked-input="form.ruleGroupsAllSelected[idx]"
                  @change="e => inputRuleGroupChanged(idx, e.target?.checked)" />
              </span>
            </div>

            <ul class="grid grid-cols-4 py-3 px-4">
              <li v-for="rule in ruleGroup.rules" :key="rule.id" class="py-2">
                <span class="mt-0.5">
                  <checkbox-with-square-icon :is-checked-input="form.field.rules.has(rule.id)"
                    @change="e => inputRuleChanged(rule.id, e.target?.checked)" />
                </span>
                <span class="ml-3">{{ $t(`label.${rule.name}`) }}</span>
              </li>
            </ul>
          </div>
        </form>

        <div class="pt-4 mt-4 border-t border-dashed border-gray-300">
          <div class="text-center font-semibold">{{ $t('label.userList') }}</div>

          <div class="mt-4 h-52 overflow-y-auto overflow-x-auto">
            <table-simple>
              <template v-slot:head>
                <th class="py-2 px-3">
                  <checkbox-with-square-icon :is-checked-input="false" @inputChanged="v => { }" />
                </th>
                <table-h-item-with-sort :description="{ isVisible: true }" class="py-2 px-3">{{ $t('label.user')
                }}</table-h-item-with-sort>
                <table-h-item-with-sort :description="{ isVisible: true }" class="py-2 px-3">{{ $t('label.status')
                }}</table-h-item-with-sort>
              </template>

              <template v-slot:body>
                <tr v-for="(group, idx) in tableData.getItems()" :key="idx"
                  class="border-b cursor-pointer even:bg-white odd:bg-slate-100">
                  <td class="p-3">
                    <checkbox-with-square-icon @changed="e => e" />
                  </td>
                  <td class="p-3">@{{ group.name }}</td>
                  <td class="p-3">
                    <tag-simple v-if="group.qtUsers !== 50" text="Ativo" class="text-xs text-slate-50 bg-green-600" />
                    <tag-simple v-else text="Inativo" class="text-xs text-slate-50 bg-red-500" />
                  </td>
                </tr>
              </template>
            </table-simple>
          </div>
        </div>
      </div>

      <div class="flex justify-between mt-8">
        <div class="grid grid-cols-2">
          <div v-if="oldGroup.data" class="relative group text-sm text-red-500">
            <tag-simple :text="$t('label.delete')" class="absolute hidden -top-7 border-red-500 group-hover:inline" />

            <a class="cursor-pointer" @click="() => { console.log('Click restaurar') }">
              <icon-square class="border-red-500 text-xs" :description="{ icon: 'fa-solid fa-trash' }" />
            </a>
          </div>

          <div v-if="openGroupHasChanged" class="relative ml-1.5 group text-sm text-green-600">
            <tag-simple :text="$t('label.restore')" class="absolute hidden -top-7 border-green-600 group-hover:inline" />

            <a class="cursor-pointer" @click="() => { console.log('Click restaurar') }">
              <icon-square class="border-green-600 text-xs" :description="{ icon: 'fa-solid fa-rotate-left' }" />
            </a>
          </div>
        </div>

        <div class="">
          <button-with-icon
            :description="{ label: 'Salvar', icon: { icon: 'fa-solid fa-floppy-disk' }, isDisabled: false }" />
        </div>
      </div>
    </app-box>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import AppBox from '@/components/boxes/AppBox.vue';
import ButtonWithIcon from '@/components/buttons/ButtonWithIcon.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import IconSquare from '@/components/visual/IconSquare.vue';
import IconSquareSmall from '@/components/visual/IconSquareSmall.vue';
import Table from '@/models/Table';
import TableHItemWithSort from '@/components/table/TableHItemWithSort.vue';
import TableSimple from '@/components/table/TableSimple.vue';
import TagSimple from '@/components/visual/TagSimple.vue';


interface IAdminGroup {
  id: number;
  name: string;
  rules: number[]
}

interface IAdminGroupShort {
  id: number;
  name: string;
  qtUsers: number;
}


export default defineComponent({
  name: 'ManagementGroupPage',

  components: { AppBox, ButtonWithIcon, CheckboxWithSquareIcon, IconSquare, IconSquareSmall, TableHItemWithSort, TableSimple, TagSimple },

  data() {
    return {
      menuStore: menuItemStore(),
      tableData: new Table<IAdminGroupShort>(),

      ruleGroups: [
        { name: 'management', rules: [{ id: 0, name: 'admin' }, { id: 1, name: 'group' }] },
        { name: 'user', rules: [{ id: 2, name: 'list' }, { id: 3, name: 'add' }, { id: 4, name: 'list' }, { id: 5, name: 'list' }] },
        { name: 'nas', rules: [{ id: 6, name: 'list' }, { id: 7, name: 'add' }, { id: 8, name: 'list' }, { id: 9, name: 'list' }] }
      ],
      oldGroup: {
        data: undefined as IAdminGroup | undefined
      },
      form: {
        ruleGroupsAllSelected: [] as boolean[],
        field: {
          id: undefined as number | undefined,
          name: {
            value: '',
            error: '',
          },
          rules: new Set<number>()
        }
      }
    };
  },

  created() {
    this.menuStore.selectManagement(1);

    this.form.ruleGroupsAllSelected = [false, false, false]
    this.tableData.appendItems([
      { id: 1, name: 'Group 1', qtUsers: 15 },
      { id: 2, name: 'Group 2', qtUsers: 1 },
      { id: 3, name: 'Group 3', qtUsers: 50 },
      { id: 1, name: 'Group 1', qtUsers: 15 },
      { id: 2, name: 'Group 2', qtUsers: 1 },
      { id: 3, name: 'Group 3', qtUsers: 50 },
      { id: 1, name: 'Group 1', qtUsers: 15 },
      { id: 2, name: 'Group 2', qtUsers: 1 },
      { id: 3, name: 'Group 3', qtUsers: 50 },
      { id: 1, name: 'Group 1', qtUsers: 15 },
      { id: 2, name: 'Group 2', qtUsers: 1 },
      { id: 3, name: 'Group 3', qtUsers: 50 },
    ]);
  },

  methods: {
    setGroupToEdit(id?: number): void {
      if (id !== undefined && id === this.form.field.id) return;

      if (!id) {
        this.oldGroup.data = undefined;

        this.form.field.id = id ?? 0;
        this.form.field.name.value = '';
        this.form.field.rules = new Set<number>;
        return;
      }

      const data = this.apiGetGroupMock(id);

      this.oldGroup.data = data;
      this.form.field.id = data.id;
      this.form.field.name.value = data.name;
      this.form.field.rules = new Set<number>(data.rules);
    },

    apiGetGroupMock(id: number): IAdminGroup {

      const data = this.tableData.getItems()[id];

      return {
        id,
        name: data.name,
        rules: [1, 3, 4]
      }
    },

    inputRuleChanged(idRule: number, isChecked: boolean): void {
      var idExist = this.form.field.rules.has(idRule);

      if ((idExist && isChecked) || (!idExist && !isChecked)) return;

      !idExist && isChecked ? this.form.field.rules.add(idRule) : this.form.field.rules.delete(idRule);
    },

    inputRuleGroupChanged(idxGroup: number, isChecked: boolean): void {
      for (const rule of this.ruleGroups[idxGroup].rules)
        this.inputRuleChanged(rule.id, isChecked);
    },
  },

  computed: {
    openGroupHasNameChanged(): boolean {
      if (!this.oldGroup.data) return false;

      return this.oldGroup.data.name !== this.form.field.name.value;
    },

    openGroupHasChanged(): boolean {
      if (!this.oldGroup.data) return false;

      if (this.form.field.name.value !== this.oldGroup.data.name
        || this.form.field.rules.size !== this.oldGroup.data.rules.length) return true;

      for (const rule of this.oldGroup.data.rules)
        if (!this.form.field.rules.has(rule)) return true;

      return false;
    }
  }
});
</script>
