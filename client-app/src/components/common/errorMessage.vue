<template>
    <div class="alert alert-warning" role="alert" v-if="content.length > 0">
        {{content}}
    </div>
</template>

<script>
export default {
    name: "error-message",
    props: ['content']
}
</script>

<style>
    
</style>