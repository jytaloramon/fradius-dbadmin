import { createI18n } from 'vue-i18n';

const messages = {
  'pt-BR': {
    label: {
      add: 'Adicionar',
      dashboard: 'Dashboard',
      exit: 'Sair',
      list: 'Lista',
      merger: 'Mesclar',
      nas: 'NAS',
      users: 'Usuários',
      view: 'Visualizar'
    },
    message: {
      indexOutOfRange: 'Índice fora do intervalo: {interval}'
    }
  }
};

const i18nModule = createI18n({
  locale: 'pt-BR',
  messages
});

export { i18nModule };
