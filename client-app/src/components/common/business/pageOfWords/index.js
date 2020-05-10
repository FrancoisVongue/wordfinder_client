export default {
    updatePageAfterSubmission(currentPage, totalItems, itemsPerPage) {
        if( Math.ceil(totalItems / itemsPerPage) < currentPage)
            return Math.ceil(totalItems / itemsPerPage);
        else 
            return currentPage;
    }
}