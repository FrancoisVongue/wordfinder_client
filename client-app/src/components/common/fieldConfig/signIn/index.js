export default {
    loginField: {
        id: 'login_input',
        type: 'text',
        smallText: '',
        name: 'Login',
        rules: 'alpha|required|min:5|max:14',
        placeholder: 'Input your login, please',
        value: ''
    },
    passwordField: {
        id: 'password_input',
        type: 'password',
        smallText: '',
        name: 'Password',
        rules: 'required|min:6|max:20',
        placeholder: 'Input your password, please',
        value: ''
    }
}