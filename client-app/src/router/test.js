import word from "../components/common/Word.vue";
import addTranslation from "../components/addWordsWindow/addWords";
import search from "../components/common/SearchField.vue";

export default [
  {
    path: "/test/word",
    component: word,
    props: (route) => ({ content: route.query.content }),
  },
  {
    path: "/test/addtranslation/:words",
    component: addTranslation,
    props: true,
  },
  {
    path: "/test/search",
    component: search,
    props: true,
  },
];
