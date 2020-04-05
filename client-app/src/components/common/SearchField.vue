<template>
  <div class="input_wrapper">
    <input class="form-control input_field" type="search"
    :placeholder="placeholder" aria-label="Search"
    v-model="inputString"
    @input="checkMatches"
    @keyup.delete="checkMatches"/>
    <ul :class="{input_matches: true, input_show: matches.length > 0}">
      <li class="input_match" v-for="(match, i) in matches" 
      :key="i"
      @click="setInput(match)">{{match}}</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "SearchField",
  props: ["config"],
  data() {
    return {
      placeholder: this.config ? this.config.placeholder : "",
      inputString: "",
      tokens:  this.config ? this.config.tokens : [],
      matches: []
    }
  },
  methods: {
    checkMatches() {
      if(!this.inputString.length) return this.matches = [];
      this.matches = this.tokens.filter(t => ~t.indexOf(this.inputString))
    },
    setInput(match) {
      this.inputString = match;
      this.matches = [];
    }
  }
}
</script>

<style lang="scss" scoped>
  .input_{
    &wrapper {
      position: relative;
    }
    &matches {
      margin: 0;
      padding: 0;
      list-style-type: none;
      display: none;
      position: absolute;
      left: 0;
      right: 0;
      top: 3em;
    }
    &show{
      display: block;
    }
    &match{
      display: block;
      text-align: center;
      font-size: 1rem;
      padding: .375em .75em;
      background-color: #f8f9fa;
      border-radius: .2em;
      transition: .3s;
      &:hover {
        background-color: #0062cc;
        color: white;
        cursor: pointer;
        transition: .15s;
      }
    }
  }
</style>