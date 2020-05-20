<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
        <p class="display-4">Find new words!</p>
        <error-message :content="errorMessage"/>
        <form @submit.prevent="handleSubmit(getWords)">
            <div class="row">
                <div class="form-group col-md-6">
                    <InputField @input="getToolTip" :field-data.sync="NameField"/>
                </div>
                
                <div class="form-group col-md-12">
                    <InputField class="text-content_input" :field-data.sync="ContentField"/>
                </div>
            </div>
            
            <button class="btn btn-primary" :disabled="failed">
                <span v-if="!loading">Submit</span>
                <template v-else>
                    <span>Loading...</span>
                    <span class="spinner-grow spinner-grow-sm"
                        role="status" aria-hidden="true">
                    </span>
                </template>
            </button>
        </form>
    </ValidationObserver>
</template>

<script>
    import {mapGetters} from 'vuex'
    import business from '../common/business/searchField'
    import businessFP from '../common/business/fp'
    import InputField from '../common/InputField'
    import {ValidationObserver} from 'vee-validate'
    import fieldConfig from '../common/fieldConfig/findWords'
    import errorMessage from '@/components/common/errorMessage'

    export default {
        name: "findWords",
        data() {
            return {
                NameField: fieldConfig.NameField,
                ContentField: fieldConfig.ContentField,                
                loading: false,
                errorMessage: '',
                nameFieldName: 'Text name',
                searchTimer: {
                    timer: null
                }
            }
        },
        methods: {
            getWords() {
                this.errorMessage = '';
                this.loading = true;
                const text = {
                    Name: this.NameField.value,
                    Content: this.ContentField.value
                }
                this.$store.dispatch('getNewWords', text)
                    .then(wordsAmount => {
                        if(wordsAmount > 0)
                            this.$router.push({name: 'addWords'});
                        else 
                            this.errorMessage = 'No new words were found!'
                    })
                    .finally(() => {
                        this.loading = false;
                    })
            },
            getToolTip() {
                const value = fieldConfig.NameField.value;
                const topics = this.userTopicsInfo.map(t => t.name);
                const relED = business.relativeEditDistance.bind(business);
                
                const relEditDistanceFromValue = businessFP.curryLast(relED)(value);
                const findClose = _ => topics
                    .map(t => [relEditDistanceFromValue(t), t])
                    .sort((a, b) => b[0] - a[0]);
                const setClose = _ => {
                    const close = findClose();
                    //console.table(close);
                    fieldConfig.NameField.name = findClose()[0][1]
                }
                const stutterFind = businessFP.stutter(setClose, 450, this.searchTimer);
                
                let toolTip = topics.find(t => ~(t.toLowerCase()).indexOf(value.toLowerCase()));
                if(!toolTip)
                    stutterFind();
                
                console.log(this.nameFieldName);
                fieldConfig.NameField.name =  !(value && toolTip)
                    ? this.nameFieldName
                    : toolTip;
            },
        },
        computed: {
            ...mapGetters(['userTopicsInfo'])  
        },
        components: { ValidationObserver, InputField, errorMessage },
        
    }
</script>

<style lang="scss" >
    .text-content_input textarea{
        height: 7em;
    }
</style>