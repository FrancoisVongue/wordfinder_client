export default {
    firstNameField: {
        id: 'firstName_create',
        type: 'text',
        smallText: '',
        name: 'First name',
        rules: 'alpha|required|max:20',
        placeholder: 'Input your first name, please',
        value: ''
    },
    secondNameField: {
        id: 'secondName_create',
        type: 'text',
        smallText: '(optional)',
        name: 'Second name',
        rules: 'alpha|max:30',
        placeholder: 'Input your second name, please',
        value: ''
    },
    loginField: {
        id: 'login_create',
        type: 'text',
        smallText: '',
        name: 'Login',
        rules: 'min:5|max:14|required',
        placeholder: 'Input your login, please',
        value: ''
    },
    emailField: {
        id: 'email_create',
        type: 'text',
        smallText: '',
        name: 'Email',
        rules: 'required|email',
        placeholder: 'Input your email, please',
        value: ''
    },
    passwordField: {
        id: 'password_create',
        type: 'password',
        smallText: '',
        name: 'Password',
        rules: 'diversity:5|min:6|max:20|required',
        placeholder: 'Input your password, please',
        value: ''
    },
    get passwordRepeatField() {
        return {
            id: 'repeat_password',
            type: 'password',
            smallText: '',
            name: 'Repeat password',
            rules: 'required|sameas:' + this.passwordField.value,
            placeholder: 'Repeat your password, please',
            value: ''
        }
    }
}