.<template>
  <div class="window">
    <p class="display-4">My Words</p>

    <form class="form-inline">
      <searchField class="mr-sm-2" :config="config"/>
      <searchField class="mr-sm-2" :config="config"/>
      <searchField class="mr-sm-2" :config="config"/>
      <!-- <input class="form-control mr-sm-2" type="search" placeholder="Part of word" aria-label="Search"/>
      <input class="form-control mr-sm-2" type="search" placeholder="Text name" aria-label="Search" />
      <input class="form-control mr-sm-2" type="search" placeholder="Tags" aria-label="Search" /> -->
    </form>
  </div>
</template>

<script>
import searchField from "../common/SearchField";

export default {
  name: "myWordsWindow",
  components: {searchField},
  data() {
    return {
      config: {
        placeholder: "input",
        tokens: ["hello", "world", "nice", "extracurricular"],
        CSV: true
      }
    }
  },
};
</script>

<style lang="scss" scoped>
.form-control:focus {
  outline: none;
}
</style>