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
        <div>
          <span class="mt-2 font-semibold">{{ $t('message.groupSettingSubTitle') }}</span>

          <tag-simple v-if="oldGroup.data" :text="$t('label.edit')"
            class="float-right text-xs text-slate-50 bg-red-500" />
          <tag-simple v-else :text="$t('label.new')" class="float-right text-xs text-slate-50 bg-green-600" />
        </div>

        <form class="mt-10">
          <div class="mb-5">
            <input-text-with-feedback
              :description="{ label: $t('label.group'), icon: { icon: 'fa-solid fa-layer-group' }, placeholder: 'Admin, monitor', value: form.field.name.value }"
              @input="e => form.field.name.value = e.target.value" />

            <span>{{ form.field.name.error }}</span>
          </div>

          <div v-for="(ruleGroup, idx) in ruleGroups" :key="idx" class="pt-4 border-t border-dashed border-gray-300">
            <div class="">
              <span class="font-semibold">{{ $t(`label.${ruleGroup.name}`) }}</span>
              <span class="mt-0.5 float-right">
                <checkbox-with-square-icon :is-checked-input="form.ruleGroupsGlobalStatus[idx]"
                  @change="e => formChangeGroupRules(idx, e.target?.checked)" />
              </span>
            </div>

            <ul class="grid grid-cols-4 py-3 px-4">
              <li v-for="rule in ruleGroup.rules" :key="rule.id" class="py-2">
                <span class="mt-0.5">
                  <checkbox-with-square-icon :is-checked-input="form.field.rules.has(rule.id)"
                    @change="e => formChangeRules(rule.id, e.target?.checked)" />
                </span>
                <span class="ml-3">{{ $t(`label.${rule.name}`) }}</span>
              </li>
            </ul>
          </div>
        </form>

        <div v-if="oldGroup.data" class="pt-4 mt-4 border-t border-dashed border-gray-300">
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
          <button-with-icon :class="getClassSaveButton"
            :description="{ label: 'Salvar', icon: { icon: 'fa-solid fa-floppy-disk' }, isDisabled: form.isInSaveLoading }"
            @click="save" />
        </div>
      </div>
    </app-box>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { menuItemStore } from '@/stores/menu-item-store';
import { type IAdminGroupShort, type IAdminGroup, adminGroupGetAll, adminGroupPost } from '@/requests/admin-group-req'
import AppBox from '@/components/boxes/AppBox.vue';
import ButtonWithIcon from '@/components/buttons/ButtonWithIcon.vue';
import CheckboxWithSquareIcon from '@/components/inputs/CheckboxWithSquareIcon.vue';
import IconSquare from '@/components/visual/IconSquare.vue';
import IconSquareSmall from '@/components/visual/IconSquareSmall.vue';
import InputTextWithFeedback from '@/components/inputs/InputTextWithFeedback.vue';
import Table from '@/models/Table';
import TableHItemWithSort from '@/components/table/TableHItemWithSort.vue';
import TableSimple from '@/components/table/TableSimple.vue';
import TagSimple from '@/components/visual/TagSimple.vue';


export default defineComponent({
  name: 'ManagementGroupPage',

  components: { AppBox, ButtonWithIcon, CheckboxWithSquareIcon, IconSquare, IconSquareSmall, InputTextWithFeedback, TableHItemWithSort, TableSimple, TagSimple },

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
        ruleGroupsGlobalStatus: [] as boolean[],
        field: {
          id: undefined as number | undefined,
          name: {
            value: '',
            error: '',
          },
          rules: new Set<number>()
        },
        isInSaveLoading: false,
      }
    };
  },

  computed: {
    getClassSaveButton(): string {
      return this.form.isInSaveLoading ? 'animate-pulse cursor-progress' : '';
    },
    openGroupHasChanged(): boolean {
      if (!this.oldGroup.data) return false;

      if (this.form.field.name.value !== this.oldGroup.data.name
        || this.form.field.rules.size !== this.oldGroup.data.rules.length) return true;

      for (const rule of this.oldGroup.data.rules)
        if (!this.form.field.rules.has(rule)) return true;

      return false;
    }
  },

  created() {
    this.menuStore.selectManagement(1);

    this.form.ruleGroupsGlobalStatus = this.ruleGroups.map(() => false);
    this.tableData.appendItems(adminGroupGetAll());
  },

  methods: {
    formChangeRules(idRule: number, isChecked: boolean): void {
      this.addOrRemoveRule(idRule, isChecked);
      this.checkIfRuleGroupsHasSameState();
    },

    formChangeGroupRules(indexG: number, isChecked: boolean): void {
      for (const rule of this.ruleGroups[indexG].rules)
        this.addOrRemoveRule(rule.id, isChecked);

      this.checkIfRuleGroupsHasSameState();
    },

    formSaving(): void {
      this.form.isInSaveLoading = true;
    },

    formSaveFinished(): void {
      this.form.isInSaveLoading = false;
    },

    addOrRemoveRule(idRule: number, status: boolean): void {
      var idExist = this.form.field.rules.has(idRule);

      if ((idExist && status) || (!idExist && !status)) return;

      !idExist && status ? this.form.field.rules.add(idRule) : this.form.field.rules.delete(idRule);
    },

    checkIfRuleGroupsHasSameState(): void {
      for (let i = 0; i < this.ruleGroups.length; ++i) {
        const group = this.ruleGroups[i];
        this.form.ruleGroupsGlobalStatus[i] = group.rules.every(r => this.form.field.rules.has(r.id));
      }
    },

    setGroupToEdit(id?: number): void {
      if (id === undefined) {
        this.oldGroup.data = undefined;
        this.form.field.id = undefined;
        this.form.field.name.value = '';
        this.form.field.rules = new Set<number>();

        return;
      }

      if (id === this.oldGroup.data?.id) return;

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

    save(): void {
      const adminGroup = { name: this.form.field.name.value, rules: Array.from(this.form.field.rules.values()) };

      this.formSaving()

      const data = adminGroupPost(adminGroup);

      this.tableData.appendItems([{ id: data.id, name: data.name, qtUsers: 0 }]);

      this.formSaveFinished();
    }
  }
});
</script>
