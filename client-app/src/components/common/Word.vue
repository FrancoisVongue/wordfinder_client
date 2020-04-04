<template>
  <div class="container-fluid border word">
    <div class="row justify-content-center" v-if="wordSchema.editing">
      <div class="col-8 text-center">
        <p>
          <input type="text" class="display-4 text-center word_content_input"
            v-model="wordSchema.content">
        </p>
      </div>
    </div>
    <div class="row justify-content-center" v-else>
      <div class="col-8 text-center">
        <p class="display-4 text-center word_content_display">{{wordSchema.content}}</p>
      </div>
    </div>
    
    <div class="row">
      <p class="text-center col-sm-4 col-form-label">Translation</p>
      <p class="text-center col-sm-4 col-form-label">Tags</p>
      <p class="text-center col-sm-4 col-form-label">Notes</p>
    </div>
      
    <template v-if="wordSchema.editing">
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
          v-model="wordSchema.notes">
          </textarea>
        </div>
      </div>
      
      <div class="form-group row justify-content-center">
        <div class="col-sm-4 text-center">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="dont_repeat"
              v-model="wordSchema.familiar">
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
          <p class="form-control" id="translation">
            <span v-if="wordSchema.translations.length == 0">No Translation</span>
            <span v-for="(translation, i) in wordSchema.translations" :key="i">{{translation}}</span>
          </p>
        </div>
        <div class="col-sm-4">
          <p class="form-control" id="tags">
            <span v-if="wordSchema.tags.length == 0">No Tags</span>
            <span v-for="(tag, i) in wordSchema.tags" :key="i">
              <span v-if="i > 0">, </span>
              {{tag}}
            </span>
          </p>
        </div>
        <div class="col-sm-4">
          <p class="form-control" id="notes">
            <span v-if="wordSchema.notes.length == 0">No Description</span>
            {{wordSchema.notes}}
          </p>
        </div>
      </div>
      
      <div class="form-group row justify-content-center">
        <div class="col-sm-4 text-center">
          <div class="custom-control custom-checkbox">
            <input type="checkbox" class="custom-control-input" id="is_repeated" disabled
            :checked="!wordSchema.familiar">
            <label class="custom-control-label" for="is_repeated"><span v-if="wordSchema.familiar">Is not</span> Repeated!</label>
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
  name: 'Word',
  props: ['word'],
  data() {
    return {
      wordSchema: {
        content: this.word.content,
        tags: this.word.tags || [],
        translations: this.word.translations || [],
        notes: this.word.notes || "",
        familiar: !!this.word.familiar,
        editing: !!this.word.editing,
      },
      tagsString: "",
      translationsString: ""
    }
  },
  methods: {
    autoGrow(e) {
      const inputElement = e.target;
      inputElement.style.height = (inputElement.scrollHeight + 2)+"px";
    },
    beginEdition(e) {
      this.wordSchema.editing = true;
    },
    endEdition(e) {
      this.wordSchema.tags = this.tagsString
              .split(',')
              .filter(t => t.trim())
              .map(tag => tag.trim());

      this.wordSchema.translations = this.translationsString
              .split(',')
              .filter(t => t.trim())
              .map(translation => translation.trim());

      this.word = this.wordSchema;

      this.wordSchema.editing = false;
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
    &input, &display { line-height: 1.5 }
    &input{
      border: 1px solid transparent;
      border-bottom: 1px solid rgba(0, 0, 0, 0.23)
    }
    &display { border: 2px solid transparent; }
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