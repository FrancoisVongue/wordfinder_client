<template>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-center">
            <li class="page-item" :class="{disabled : currentPage <= 1}">
                <a class="page-link" href="#" tabindex="-1"
                   @click="changePage(a => a - 1)">Previous</a>
            </li>

            <li class="page-item" v-for="page in pages"
                :key="page"
                :class="{disabled : isCurrentPage(page), current: isCurrentPage(page)}">
                <a class="page-link" href="#" @click="changePage(a => page)">
                    {{page}}
                </a>
            </li>

            <li class="page-item" :class="{disabled : currentPage >= totalPages}">
                <a class="page-link" href="#"
                   @click="changePage(a => a + 1)">Next</a>
            </li>
        </ul>
    </nav>
</template>

<script>
    export default {
        name: "Pagination",
        props: ['paginationInfo'],
        data() {
            return {
                maxPagesView: 7
            }
        },
        computed: {
            totalPages() {
                return Math.ceil(this.paginationInfo.itemsAmount / 
                    this.paginationInfo.perPage);
            },
            currentPage() {
                return this.paginationInfo.page;
            },
            pages() {
                const block = Math.floor(this.maxPagesView / 2);
                const pages = [];
                
                if(this.totalPages <= this.maxPagesView) {
                    return this.getArrayOfNumbers(1, this.totalPages);
                }
                if( this.currentPage <= block ) {
                    return this.getArrayOfNumbers(1, this.maxPagesView);
                }
                if( this.currentPage + block >= this.totalPages ) {
                    return this.getArrayOfNumbers(this.totalPages - this.maxPagesView,
                        this.totalPages);
                }
                else {
                    return this.getArrayOfNumbers(this.currentPage - block,
                        this.currentPage + block)
                }
            }
        },
        methods: {
            isCurrentPage(page) {
                return page === this.paginationInfo.page;
            },
            changePage(predicate) {
                let a = this.currentPage;
                let b = predicate(a);
                
                this.paginationInfo.page += b - a;
            },
            getArrayOfNumbers(a, b) {
                const array = [];
                for(let i = a ; i <= b; i++)
                    array.push(i);
                return array;
            }
        },
    }
</script>

<style lang="scss" scoped>
    .current .page-link {
        background-color: #0062cc;
        color: ghostwhite;
    }
    .pagination * {
        box-shadow: none;
    }
</style>