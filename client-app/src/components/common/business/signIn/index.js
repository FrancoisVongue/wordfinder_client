export default {
    async signIn() {
        const credentials = {
            login: this.loginField.value,
            password: this.passwordField.value
        };
        
        await this.$store.dispatch('signIn', credentials);
        this.$router.push({name:'myWords'});
    }
}