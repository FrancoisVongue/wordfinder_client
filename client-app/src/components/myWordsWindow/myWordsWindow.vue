.<template>
  <div class="window">
    <p class="display-4">My Words</p>
    
    <form class="form-inline">
      <input class="form-control mr-sm-2" type="search" placeholder="Part of word" aria-label="Search">
      <input class="form-control mr-sm-2" type="search" placeholder="Text name" aria-label="Search">
      <input class="form-control mr-sm-2" type="search" placeholder="Tags" aria-label="Search">
    </form>
  </div>
</template>

<script>
export default {
  name: 'myWordsWindow',
  
}
</script>

<style lang="scss" scoped>
  .form-control:focus {
    outline: none;
  }
</style>