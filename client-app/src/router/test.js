import word from '../components/common/Word.vue'
import addTranslation from "../components/addWordsWindow/addWords";

export default [
  {
    path: '/test/word/:content',
    component: word,
    props: true
  },{
    path: '/test/addtranslation/:words',
    component: addTranslation,
    props: true
  },
]