<template>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-center">
            <li class="page-item" :class="{disabled : paginationInfo.page <= 1}">
                <a class="page-link" href="#" tabindex="-1"
                   @click="--paginationInfo.page">Previous</a>
            </li>

            <li class="page-item" v-for="page in pagination"
                :key="page"
                :class="{disabled : isCurrentPage(page), current: isCurrentPage(page)}">
                <a class="page-link" href="#" @click="paginationInfo.page = page">
                    {{page}}
                </a>
            </li>

            <li class="page-item" :class="{disabled : paginationInfo.page >= totalPages}">
                <a class="page-link" href="#"
                   @click="++paginationInfo.page">Next</a>
            </li>
        </ul>
    </nav>
</template>

<script>
    export default {
        name: "Pagination",
        props: ['paginationInfo'],
        computed: {
            totalPages() {
                return Math.ceil(this.paginationInfo.items.length / 
                    this.paginationInfo.perPage)
            },
            pagination() {
                const totalPages = this.totalPages;
                const currentPage = this.paginationInfo.page;
                const pagesToDisplay = 3 * 2; // except current
                const wing = pagesToDisplay / 2;
                const pagination = [];
                
                const toRight = totalPages - currentPage;
                const toLeft = currentPage - 1;
                let rightWing = wing;
                let leftWing = wing;
                
                if(currentPage < totalPages / 2) {
                    if(toLeft < wing) {
                        rightWing += wing - toLeft;
                        leftWing = toLeft;
                    }
                } else  {
                    if(toRight < wing) {
                        leftWing += wing - toRight;
                        rightWing = toRight;
                    }
                }
                
                for(let i = currentPage - leftWing; i <= currentPage + rightWing; i++) {
                    pagination.push(i);
                }
                
                
                return pagination;
            }
        },
        methods: {
            isCurrentPage(page) {
                return page === this.paginationInfo.page;
            }
        },
    }
</script>

<style lang="scss" scoped>
    .current .page-link {
        background-color: #0062cc;
        color: ghostwhite;
    }
</style>