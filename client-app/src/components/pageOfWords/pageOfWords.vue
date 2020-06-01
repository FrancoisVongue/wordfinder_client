<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
        <Word v-for="word in WordsForCurrentPage" 
             @updatedWord="updatedWord"
            :key="word.additionTime" :word.sync="word" :fresh="fresh"/>
        <pagination :pagination-info.sync="pagination"/>
        <template v-if="fresh">
            <button type="submit" class="btn mr-2 btn-primary" :disabled="failed"
                @click.prevent="handleSubmit(submit)">
                Submit
            </button>
            <button class="btn btn-secondary" @click.prevent="discardWords">
                Cancel
            </button>
        </template>
    </ValidationObserver>
</template>

<script>
    import pagination from "../common/Pagination";
    import business from "../common/business/pageOfWords"
    import Word from '../common/Word'
    import {ValidationObserver} from 'vee-validate'
    export default {
        props: ['words', 'fresh'],
        components: {Word, pagination, ValidationObserver},
        data() {
            return {
                pagination: {
                    itemsAmount: this.words.length,
                    perPage: 5,
                    page: 1
                }
            }
        },
        watch: {
            "pagination.itemsAmount": function (amount) {
                this.pagination.page = 
                    business.updatePageAfterSubmission(this.pagination.page, 
                        amount,
                        this.pagination.perPage);
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
            updatedWord() {
                this.$emit("updatedWord");
            },
            submit() {
                console.log(this.WordsForCurrentPage);
                //this.$emit('submit', this.WordsForCurrentPage);
            },
            discardWords() {
                this.$emit('discard');
            }
        }
    }
</script>

<style lang="scss" scoped>

</style>