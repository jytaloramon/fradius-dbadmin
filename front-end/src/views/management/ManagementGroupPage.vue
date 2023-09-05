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

      <div class="mt-8 overflow-y-auto overflow-x-auto">
        <app-table :pagination="{ actual: 1, max: 2 }">
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

    <app-box class="flex flex-col justify-between w-1/2 md:h-[560px] ml-6 overflow-y-auto overflow-x-auto">
      <div class="text-sm">
        <div class="mb-4">
          <div class="font-semibold">
            <label for="">{{ $t('label.group') }}: </label>
            <input class="focus:outline-none" type="text" value="Grupo 1">
            <p class="inline-block float-right py-0.5 px-2 text-xs text-slate-50 bg-red-500 rounded-md">
              Alterado: Grupo 1
            </p>
          </div>

          <p class="mt-1 text-sm">Configure permissões/usuários deste grupo.</p>
        </div>

        <div v-for="(ruleGroup, idx) in ruleGroups" :key="idx" class="pt-4 border-t border-dashed border-gray-300">
          <div class="">
            <p class="inline font-semibold">{{ $t(`label.${ruleGroup.name}`) }}</p>
            <span class="mt-0.5 float-right">
              <checkbox-with-square-icon :is-checked-input="adminGroupOpen.ruleGroupsAllSelected[idx]"
                @inputChanged="v => inputRuleGroupChanged(ruleGroup.rules, v)" />
            </span>
          </div>

          <ul class="grid grid-cols-4 py-3 px-4">
            <li v-for="rule in ruleGroup.rules" :key="rule.id" class="py-2">
              <span class="mt-0.5"><checkbox-with-square-icon :is-checked-input="adminGroupOpen.actualRule.has(rule.id)"
                  @inputChanged="v => inputRuleChanged(rule.id, v)" /></span>
              <span class="ml-3">{{ $t(`label.${rule.name}`) }}</span>
            </li>
          </ul>
        </div>

        <div class="pt-4 mt-4 border-t border-dashed border-gray-300">
          <p class="text-center font-semibold">Lista de Usuários</p>

          <div class="mt-4 h-52 overflow-y-auto overflow-x-auto">
            <app-table :pagination="{ actual: 1, max: 2 }">
              <template v-slot:head>
                <table-head-item-with-sort class="py-2 px-3">
                  <checkbox-with-square-icon :is-checked-input="false" @inputChanged="v => { }" />
                </table-head-item-with-sort>
                <table-head-item-with-sort class="py-2 px-3">Usuário</table-head-item-with-sort>
                <table-head-item-with-sort class="py-2 px-3">Status</table-head-item-with-sort>
              </template>

              <template v-slot:body>
                <tr v-for="(group, idx) in tableData.getItems()" :key="idx"
                  class="border-b cursor-pointer even:bg-white odd:bg-slate-100">
                  <td class="p-3">
                    <checkbox-with-square-icon :is-checked-input="false" @inputChanged="v => { }" />
                  </td>
                  <td class="p-3">@{{ group.name }}</td>
                  <td class="p-3">
                    <p v-if="group.qtUsers !== 50"
                      class="inline-block py-0.5 px-2 text-xs text-slate-50 bg-green-600 rounded-md">
                      Ativo
                    </p>
                    <p v-else class="inline-block py-0.5 px-2 text-xs text-slate-50 bg-red-500 rounded-md">
                      Inativo
                    </p>
                  </td>
                </tr>
              </template>
            </app-table>
          </div>
        </div>
      </div>

      <div class="mt-8">
        <button-with-loading
          :button-with-loading-desc="{ label: 'Salvar', icon: { icon: 'fa-solid fa-floppy-disk' }, isLoading: adminGroupOpen.isChanged }" />
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


interface IAdminGroup {
  id: number;
  name: string;
  qtUsers: number;
}

interface IRule {
  id: number;
  name: string
}

interface IRuleGroup {
  name: string,
  rules: IRule[]
}


export default defineComponent({
  name: 'ManagementGroupPage',

  components: { AppBox, AppTable, ButtonWithLoading, CheckboxWithSquareIcon, TableHeadItemWithSort },

  data() {
    return {
      menuStore: menuItemStore(),
      tableData: new Table<IAdminGroup>(),
      ruleGroups: [
        { name: 'management', rules: [{ id: 0, name: 'admin' }, { id: 1, name: 'group' }] },
        { name: 'user', rules: [{ id: 2, name: 'list' }, { id: 3, name: 'add' }, { id: 4, name: 'list' }, { id: 5, name: 'list' }] },
        { name: 'nas', rules: [{ id: 6, name: 'list' }, { id: 7, name: 'add' }, { id: 8, name: 'list' }, { id: 9, name: 'list' }] }
      ],
      adminGroupOpen: {
        ruleGroupsAllSelected: [false, false, false],
        beforeRules: new Set<number>(),
        actualRule: new Set<number>(),
        isChanged: false
      }
    };
  },

  created() {
    this.menuStore.selectManagement(1);

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

  watch: {

  },

  methods: {
    inputRuleChanged(id: number, isChecked: boolean): void {
      var idExist = this.adminGroupOpen.actualRule.has(id);

      if ((idExist && isChecked) || (!idExist && !isChecked)) return;

      !idExist && isChecked ? this.adminGroupOpen.actualRule.add(id) : this.adminGroupOpen.actualRule.delete(id);

      this.openRulesHaveChanged();
      this.k();
    },

    inputRuleGroupChanged(rules: IRule[], isChecked: boolean): void {
      for (const rule of rules) this.inputRuleChanged(rule.id, isChecked);

      console.log(this.adminGroupOpen.isChanged)
    },

    k(): void {
      for (var i = 0; i < this.ruleGroups.length; ++i) {
        const ruleGroup = this.ruleGroups[i];

        var idx = 0;

        while (idx < ruleGroup.rules.length && this.adminGroupOpen.actualRule.has(ruleGroup.rules[idx].id)) ++idx;

        this.adminGroupOpen.ruleGroupsAllSelected[i] = idx === ruleGroup.rules.length;
      }
    },

    openRulesHaveChanged(): void {
      if (this.adminGroupOpen.beforeRules.size !== this.adminGroupOpen.actualRule.size) {
        this.adminGroupOpen.isChanged = true;
        return;
      };

      for (const rule of this.adminGroupOpen.beforeRules.values())
        if (!this.adminGroupOpen.actualRule.has(rule)) {
          this.adminGroupOpen.isChanged = true;
          return;
        }

      this.adminGroupOpen.isChanged = false;
    }
  }
});
</script>
