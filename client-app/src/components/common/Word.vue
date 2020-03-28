<template>
  <div class="container-fluid border word">
    <div class="row justify-content-center" v-if="word.editing">
      <div class="col-8 text-center">
        <p>
          <input type="text" class="display-4 text-center word_content_input"
            v-model="word.content">
        </p>
      </div>
    </div>
    <div class="row justify-content-center" v-else>
      <div class="col-8 text-center">
        <p class="display-4 text-center word_content_display">{{word.content}}</p>
      </div>
    </div>
    
    <div class="row">
      <p class="text-center col-sm-4 col-form-label">Translation</p>
      <p class="text-center col-sm-4 col-form-label">Tags</p>
      <p class="text-center col-sm-4 col-form-label">Notes</p>
    </div>
      
    <template v-if="word.editing">
      <div class="form-group row">
        <div class="col-sm-4">
          <input type="text" class="form-control"
          id="translation_input" placeholder="translation"
          v-model="translationsString">
        </div>
        <div class="col-sm-4">
          <input type="text" class="form-control"
          id="tags_input" placeholder="tags"
          v-model="tagsString">
        </div>
        <div class="col-sm-4">
          <textarea class="form-control" id="notes_input"
          placeholder="description" rows="1"
          @input="autoGrow"
          v-bind="word.notes">
          </textarea>
        </div>
      </div>
      
      <div class="form-group row justify-content-center">
        <div class="col-sm-4 text-center">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="dont_repeat"
              v-model="word.familiar">
            <label class="custom-control-label" for="dont_repeat">Don't repeat!</label>
          </div>
        </div>
      </div>
      <button type="button" class="btn btn-sm btn-success edition-control_button"
        @click="endEdition">
        Save changes
      </button>
    </template>
    
    <template v-else>
      <div class="form-group row">
        <div class="col-sm-4">
          <p class="form-control" id="translation">translation</p>
        </div>
        <div class="col-sm-4">
          <p class="form-control" id="tags">tags</p>
        </div>
        <div class="col-sm-4">
          <p class="form-control" id="notes">description</p>
        </div>
      </div>
      
      <div class="form-group row justify-content-center">
        <div class="col-sm-4 text-center">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="is_repeated" disabled
            :checked="!word.familiar">
            <label class="custom-control-label" for="is_repeated">Repeated!</label>
          </div>
        </div>
      </div> 
      
      <button type="button" class="btn btn-sm btn-warning edition-control_button"
        @click="beginEdition">Edit word
      </button>
    </template>
    
  </div>
</template>

<script>
export default {
  name: 'foundWord',
  props: ['content'],
  data() {
    return {
      word: {
        content: this.content,
        tags: [],
        translations: [],
        notes: "",
        familiar: false,
        editing: false,
      },
      tagsString: "",
      translationsString: ""
    }
  },
  methods: {
    autoGrow(e) {
      const inputElement = e.target;
      inputElement.style.height = (inputElement.scrollHeight + 3)+"px";
    },
    beginEdition(e) {
      this.word.editing = true;
    },
    endEdition(e) {
      this.word.editing = false;
    }
  }
}
</script>

<style lang="scss" scoped>
.word {
  padding: 1em;
  margin: 1em 0;
  position: relative;
  &_content_ {
    &input{ border: 1px solid #ced4da; }
    &display { border: 2px solid rgba(255,255,255,.5); }
  }
}
.form-control {
  height: initial;
  margin: 0;
  overflow: hidden;
}
.edition-control_button {
  position: absolute;
  bottom: 0;
  right: 0;
}
</style>