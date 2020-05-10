import store from '../../../../store'
import fieldConfig from '../../fieldConfig/signUp'
import dictionary from '@/components/signUpWindow/processedDictionary'

export default {
    signUp(lexiconSize) {
        const words = dictionary.slice(0, lexiconSize)
                .map(w => ({content: w}));
                
        const user = {
            firstName: fieldConfig.firstNameField.value,
            lastName: fieldConfig.secondNameField.value,
            login: fieldConfig.loginField.value,
            email: fieldConfig.emailField.value,
            password: fieldConfig.passwordField.value,
            words: words
        };
        
        return store.dispatch('signUp', user);
    }
}