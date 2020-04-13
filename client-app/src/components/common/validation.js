import {ValidationProvider, extend} from 'vee-validate';
import {email, required, alpha, } from 'vee-validate/dist/rules';

extend('min', {
    validate(value, args) {
        return value.length >= args.min;
    },
    params: ['min'],
    message: "The {_field_} field must be at least {min} characters long."
});
extend('max', {
    validate(value, args) {
        return value.length <= args.max;
    },
    params: ['max'],
    message: "The {_field_} field must be at most {max} characters long."
});
extend('diversity', {
    validate(value, args) {
        const uniqueChars = value.split('').reduce((prev, v, i, arr) => {
            const unique = arr.indexOf(v) == i;
            return prev + (unique ? 1 : 0);
        }, 0);

        return uniqueChars >= args.min;
    },
    params: ['min'],
    message: "The {_field_} field must have at least {min} unique characters."
});
extend('sameas', {
    validate(value, args) {
        return value == args.pass;
    },
    params: ['pass'],
    message: "Passwords don't match."
});
extend('email', {
    ...email,
    message: "Invalid email"
});
extend('required', {
    ...required,
    message: "Required field"
})
extend('alpha', {
    ...alpha,
    message: "Field may only contain letters."
})

export default ValidationProvider;