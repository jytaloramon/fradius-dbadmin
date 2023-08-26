<template>
  <app-box class="w-[420px] m-auto">
    <h1 class="text-base text-center font-bold">{{ headerTexts.title }}</h1>
    <h2 class="mt-2 text-center text-base">{{ headerTexts.message }}</h2>
    <h3
      class="py-2 mt-2 text-sm text-center before:float-left before:w-[130px] before:mt-3 before:border-dashed before:border-[1px]  after:float-right after:w-[130px] after:mt-3 after:border-dashed after:border-[1px]">
      {{ headerTexts.subtitle }}
    </h3>

    <div class="mt-3">
      <form>
        <input-text-with-left-icon :input-desc="form.username"
          @input-changed="value => { form.username.data.value = value }" />

        <input-text-with-left-icon :input-desc="form.password" class="mt-5"
          @input-changed="value => { form.password.data.value = value }" />

        <div class="mt-3">
          <div class="ml-1 text-sm text-gray-600 hover:text-gray-800">
            <router-link to="recovery">
              {{ $t('label.forgotYourPassword') }}?
            </router-link>
          </div>

          <button-with-loading :button-with-loading-desc="buttonSubmit" class="mt-5" />
        </div>
      </form>
    </div>
  </app-box>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppBox from '@/components/boxes/AppBox.vue';
import ButtonWithLoading from '@/components/buttons/ButtonWithLoading.vue'
import InputTextWithLeftIcon from '@/components/inputs/InputTextWithLeftIcon.vue'


export default defineComponent({
  name: 'AccountSigninPage',

  components: { AppBox, ButtonWithLoading, InputTextWithLeftIcon },

  data() {
    return {
      headerTexts: {
        title: this.$t('message.loginTitle'),
        message: this.$t('message.welcome'),
        subtitle: this.$t('label.logIn')
      },
      form: {
        username: {
          label: 'Username',
          placeholder: 'ramon04, mary_64',
          icon: 'fa-regular fa-user',
          data: {
            value: '',
            error: ''
          }
        },
        password: {
          type: 'password',
          label: 'Password',
          placeholder: '**** ****',
          icon: 'fa-solid fa-lock',
          data: {
            value: '',
            error: ''
          }
        }
      },
      buttonSubmit: {
        label: 'Entrar',
        icon: {
          icon: 'fa-solid fa-right-to-bracket',
        },
        isLoading: false
      }
    };
  }
});
</script>
