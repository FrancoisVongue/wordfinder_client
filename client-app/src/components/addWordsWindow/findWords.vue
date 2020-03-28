<template>
  <form>
    <div class="form-group">
      <label for="text-name_input">Text Name</label>
      <input type="text" id="text-name_input" 
      class="form-control" placeholder="Star Wars"
      v-model="text.Name">
    </div>
    <div class="form-group">
      <label for="text-content_input">Text</label>
      <textarea class="form-control" id="text-content_input" rows="5"
      placeholder="The Imperial Forces under orders from cruel Darth Vader..."
      v-model="text.Content"></textarea>
    </div>
    <button class="btn btn-primary" @click="getWords">Submit</button>
  </form>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: "findWords",
  data() {
    return {
      text : {
        Name: '',
        Content: ''
      }
    }
  },
  methods: {
    getWords() {
      if(this.text.Name && this.text.Content) {
        this.$store.dispatch('updateWords', this.text)
          .then(() => 
            this.$router.push({name: 'addTranslation'}));
      }
    }
  },
  computed: {
    ...mapGetters([
      'foundWords'
    ])
  },
  beforeRouteEnter (to, from, next) {
    next(vm => {
      if(vm.foundWords.length > 0)
        next({name: "addTranslation"});
      else 
        next();
    })
  }
}
</script>

<style lang="scss">

</style>