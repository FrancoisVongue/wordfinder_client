<template>
    <ValidationObserver v-slot="{ failed, handleSubmit }">
        <p class="display-4">Find new words!</p>
        <form @submit.prevent="handleSubmit(getWords)">
            <div class="form-group">
                <label for="text-name_input">Text Name</label>
                <ValidationProvider name="text name" rules="alpha|required" v-slot="{errors}">
                    <input type="text" id="text-name_input"
                           class="form-control" placeholder="Star Wars"
                           v-model="text.Name">
                    <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                    <small class="info form-text text-muted validation_info" v-else> </small>
                </ValidationProvider>
            </div>
            
            <div class="form-group">
                <label for="text-content_input">Text</label>
                <ValidationProvider name="text content" rules="alpha|required|min:3" v-slot="{errors}">
                    <textarea class="form-control" id="text-content_input" rows="5"
                              placeholder="The Imperial Forces under orders from cruel Darth Vader..."
                              v-model="text.Content"></textarea>
                    <small class="info form-text error validation_info" v-if="errors[0]">{{errors[0]}}</small>
                    <small class="info form-text text-muted validation_info" v-else> </small>
                </ValidationProvider> 
            </div>
            
            <button class="btn btn-primary" :disabled="failed">
                <span v-if="!loading">Submit</span>
                <span v-else class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true">
                  Loading...
                </span>
            </button>
        </form>
    </ValidationObserver>
</template>

<script>
    import {mapGetters} from 'vuex'
    import ValidationProvider from '../common/validation';
    import {ValidationObserver} from 'vee-validate'

    export default {
        name: "findWords",
        data() {
            return {
                text: {
                    Name: '',
                    Content: ''
                },
                loading: false
            }
        },
        methods: {
            getWords(e) {
                e.preventDefault();
                if (this.text.Name && this.text.Content) {
                    this.loading = true;
                    this.$store.dispatch('getNewWords', this.text)
                        .then(() => {
                            this.loading = false;
                            this.$router.push({name: 'addTranslation'});
                        });
                }
            }
        },
        computed: {
            ...mapGetters([
                'foundWords'
            ])
        },
        components: {
            ValidationProvider,
            ValidationObserver
        },
        beforeRouteEnter(to, from, next) {
            next(vm => {
                if (vm.foundWords.length > 0)
                    next({name: "addTranslation"});
                else
                    next();
            })
        }
    }
</script>

<style lang="scss">

</style>