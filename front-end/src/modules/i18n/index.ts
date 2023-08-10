import { createI18n } from 'vue-i18n';

const messages = {
  'pt-BR': {
    label: {
      add: 'Adicionar',
      dashboard: 'Dashboard',
      exit: 'Sair',
      merger: 'Mesclar',
      users: 'Usuários',
      view: 'Visualizar'
    }
  }
};

const i18nModule = createI18n({
  locale: 'pt-BR',
  messages
});

export { i18nModule };
