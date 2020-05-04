<template>
    <div>
        <Word v-for="word in WordsForCurrentPage" 
            :key="word.additionTime" :word.sync="word" :fresh="fresh"/>
        <pagination :pagination-info.sync="pagination"/>
        <template v-if="fresh">
            <button type="submit" class="btn mr-2 btn-primary"
                @click="submit">Submit</button>
            <button class="btn btn-secondary" @click.prevent="discardWords">Cancel</button>
        </template>
    </div>
</template>

<script>
    import pagination from "../common/Pagination";
    import Word from '../common/Word'

    export default {
        props: ['words', 'pageNumber', 'fresh'],
        components: {Word, pagination},
        data() {
            return {
                pagination: {
                    itemsAmount: this.words.length,
                    perPage: 5,
                    page: this.pageNumber
                }
            }
        },
        computed: {
            WordsForCurrentPage() {
                const start = (this.pagination.page - 1) * this.pagination.perPage;
                const end = start + this.pagination.perPage;
                const words = this.words.slice(start, end);
    
                return words;
            }
        },
        methods: {
            submit() {
                this.$emit('submit', this.WordsForCurrentPage);
            },
            discardWords() {
                this.$emit('discard');
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>