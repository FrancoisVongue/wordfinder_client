<template>
    <form>
        <p class="display-4">Add new words!</p>
        <p class="display-4">Found {{foundWords.length}} words in Text: "{{textName}}"</p>
        <word v-for="(word, index) in words"
              :key="index"
              :word.sync="word"/>
        <button class="btn btn-primary" @click="submitWords">Submit</button>
        <button class="btn btn-warning" @click="discardWords">Cancel</button>
    </form>
</template>

<script>
    import word from '../common/Word'
    import {mapGetters} from 'vuex'

    export default {
        name: "addTranslation",
        components: {word},
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