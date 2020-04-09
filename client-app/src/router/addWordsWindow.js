import findWordsWindow from '../components/addWordsWindow/addWordsWindow.vue'
import findWords from '../components/addWordsWindow/findWords.vue'
import addTranslation from '../components/addWordsWindow/addWords.vue'

export default { 
  path: '/', 
  component: findWordsWindow,
  props: (route) => ({currentWindow: route.name}),
  children: [
    // todo delete {
    //   name: 'findWords',
    //   path: 'find-words',
    //   component: findWords,
    // },
    {
      name: 'addTranslation',
      path: 'add-translation', 
      component: addTranslation,
    },
    {
      path: '',
      name: 'supplyText',
      component: findWords,
    }
  ]
}