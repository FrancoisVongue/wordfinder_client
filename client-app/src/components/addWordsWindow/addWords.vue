<template>
<ValidationObserver v-slot="{ failed, handleSubmit }">
    <form @submit.prevent="handleSubmit(submitWords)">
        <p class="display-4">Add new words!</p>
        <p class="display-4">Found {{foundWords.length}} words in Text: "{{textName}}"</p>
        <word v-for="(word, index) in foundWords"
              :key="index"
              :word.sync="word"/>
        <button type="submit" class="btn mr-2 btn-primary">Submit</button>
        <button class="btn btn-secondary" @click="discardWords">Cancel</button>
    </form>
</ValidationObserver>
</template>

<script>
    import word from '../common/Word'
    import {ValidationObserver} from 'vee-validate'
    import {mapGetters} from 'vuex'

    export default {
        name: "addTranslation",
        components: {word, ValidationObserver},
        data() {
            return {
                words: []
            }
        },
        created() {
            this.words = [...this.foundWords];
        },
        methods: {
            submitWords() {
                this.$store.dispatch('submitWords', this.words);
            },
            discardWords() {
                this.words = [];
                this.$store.dispatch('discardWords')
                    .then(() => this.$router.push({name: 'supplyText'}));
            }
        },
        computed: {
            ...mapGetters([
                'foundWords',
                'textName'
            ])
        }
    }
</script>

<style>

</style>