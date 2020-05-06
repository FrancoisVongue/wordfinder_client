import store from '../../../../store'
import fieldConfig from '../../fieldConfig/signUp'

export default {
    signUp() {
        const user = {
            firstName: fieldConfig.firstNameField.value,
            lastName: fieldConfig.secondNameField.value,
            login: fieldConfig.loginField.value,
            email: fieldConfig.emailField.value,
            password: fieldConfig.passwordField.value,
            words: null
        };
        return store.dispatch('signUp', user);
    }
}