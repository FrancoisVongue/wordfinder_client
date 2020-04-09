<template>
    <form>
        <p class="display-4">Add new words!</p>
        <p class="display-4">Found {{foundWords.length}} words in Text: "{{textName}}"</p>
        <word v-for="(word, index) in foundWords"
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
            return {}
        },
        methods: {
            submitWords() {
            },
            discardWords() {
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