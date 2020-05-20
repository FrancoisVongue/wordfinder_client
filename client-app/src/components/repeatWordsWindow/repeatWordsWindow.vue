<template>
    <div class="window">
        <form @submit.prevent="submitWords">
            <p class="display-4 mb-4">Repeat words</p>
            <p class="alert alert-secondary" v-show="failed">
                Please, type correct translations
            </p>
            
            <div v-if="loading" class="d-flex justify-content-center auth_spinner align-items-center">
                <div class="spinner-grow" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
            <div v-else v-for="word in words" :key="word.content"
                class="container">
                <div class="row justify-center">
                    <label :for="word.content" class='badge my-badge'
                        :class="{'badge-danger': word.repeatError, 'badge-success': word.repeatCorrect}">
                        {{word.repeatHint + word.repeatError + word.repeatCorrect}}
                    </label>
                    <input type="text" class="form-control mb-3" 
                        :id="word.content"
                        v-model="word.repeatField">
                </div>
            </div>
            
            <button type="submit" class="btn mr-2 btn-primary">
                {{correctInput ? 'Next' : 'Submit'}}
            </button>
        </form>
    </div>
</template>

<script>
export default {
    name: "repeatWords",
    data() {
        return {
            words: [],
            loading: false,
            failed: false
        }
    },
    computed: {
        correctInput() {
            return this.words.every(w => w.repeatCorrect);
        }
    },
    methods: {
        submitWords() {
            if(this.correctInput) {
                this.loading = true;
                this.$store.dispatch('repeatWords', this.words)
                    .then(this.setUp)
                    .then(_ => this.loading = false);
            } 
            else {
                this.failed = !this.correctCheck();
                
                this.words = this.words.map(w => {
                    w.repeatError = w.repeatField != w.content ? ' - ' + w.content : '';
                    w.repeatCorrect = w.repeatError ? '' : ' - ✓';
                    return w;
                });
            }
        },
        correctCheck() {
            return this.words.every(w => w.repeatField == w.content);
        },
        setUp(words) {
            this.words = words.map(w => {
                w.repeatHint = w.translations.map(t => t.content).join(', ');
                w.repeatField = '';
                w.repeatError = '';
                w.repeatCorrect = '';
                return w;
            });
        }
    },
    beforeMount() {
        this.loading = true;
        this.$store.dispatch('getWordsForRepetition')
            .then(this.setUp)
            .then(_ => this.loading = false);
    }
}
</script>

<style lang="scss" scoped>
.my-badge {
    font-size: 1em;
}
.error {
    font-weight: bold;
    color: rgb(212, 90, 53);
}
.correct {
    font-weight: bold;
    color: rgb(45, 209, 127);
}
</style>