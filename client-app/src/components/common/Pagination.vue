<template>
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-center">
            <li class="page-item" :class="{disabled : paginationInfo.page <= 1}">
                <a class="page-link" href="#" tabindex="-1"
                   @click="--paginationInfo.page">Previous</a>
            </li>

            <li class="page-item" v-for="page in paginationInfo.totalPages"
                :key="page"
                :class="{disabled : isCurrentPage(page), current: isCurrentPage(page)}">
                <a class="page-link" href="#" @click="paginationInfo.page = page">{{page}}</a>
            </li>

            <li class="page-item" :class="{disabled : paginationInfo.page >= paginationInfo.totalPages}">
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
        methods: {
            isCurrentPage(page) {
                return page === this.paginationInfo.page;
            }
        }
    }
</script>

<style lang="scss" scoped>
    .current .page-link {
        background-color: #0062cc;
        color: ghostwhite;
    }
</style>