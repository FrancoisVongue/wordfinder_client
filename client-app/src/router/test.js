import word from '../components/common/Word.vue'
import addTranslation from "../components/addWordsWindow/addWords";
import search from '../components/common/SearchField.vue'

export default [{
  path: '/test/word/:content',
  component: word,
  props: true
}, {
  path: '/test/addtranslation/:words',
  component: addTranslation,
  props: true
}, {
  path: '/test/search',
  component: search,
  props: true
}, ]