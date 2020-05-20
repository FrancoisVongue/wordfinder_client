<template>
    <ValidationProvider :name="fieldData.name" :rules="fieldData.rules" v-slot="{errors}">
    <label :for="fieldData.id">{{fieldData.name}}</label>
        <textarea v-if="fieldData.type == 'textarea'"
            class="form-control"
            :id="fieldData.id" 
            :placeholder="fieldData.placeholder"
            v-model="fieldData.value"/>
        <input v-else
            @input="inputEvent"
            @blur="blur"
            ref='textInput'
            @keydown.tab="complete"
            :type="fieldData.type" class="form-control" 
            :id="fieldData.id" 
            :placeholder="fieldData.placeholder"
            v-model="fieldData.value">
        <small :class="{error: errors.length > 0}" class="info form-text validation_info">
            {{errors[0] || fieldData.smallText}}
        </small>
    </ValidationProvider>
</template>

<script>
    import ValidationProvider from './validation'
    
    export default {
        name: 'inputField',
        props: ['fieldData'],
        components: {ValidationProvider},
        methods: {
            inputEvent() {
                this.$emit('input');
            },
            complete(e) {
                e.preventDefault();
                this.$emit('complete');
                this.$refs.textInput.blur();
            },
            blur() {
                this.$emit('blur');
            }
        }
    }
</script>

<style lang="scss" scoped>
    .validation_info {
        display: inline-block;
        margin-top: 0.25em;
        line-height: 1.2rem;
    }
</style>