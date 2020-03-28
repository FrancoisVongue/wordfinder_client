import findWordsWindow from '../components/addWordsWindow/findWordsWindow.vue'
import findWords from '../components/addWordsWindow/findWords.vue'
import addTranslation from '../components/addWordsWindow/addTranslation.vue'

export default { 
  path: '/', 
  component: findWordsWindow,
  children: [
    {
      name: 'findWords',
      path: 'find-words',
      component: findWords,
    },
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