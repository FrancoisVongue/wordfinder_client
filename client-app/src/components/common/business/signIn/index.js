import credentialsFields from '../../fieldConfig/signIn'
import store from '../../../../store'

export default {
    signIn() {
        const credentials = {
            login: credentialsFields.loginField.value,
            password: credentialsFields.passwordField.value
        };
        
        return store.dispatch('signIn', credentials);
    }
}