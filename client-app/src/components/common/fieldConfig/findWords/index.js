export default {
    NameField: {
        id: 'text-name_input',
        type: 'text',
        smallText: '',
        name: 'Text name',
        rules: 'alpha|required|min:3',
        placeholder: 'Input the name of the text, plese',
        value: ''
    },
    ContentField: {
        id: 'text-content_input',
        type: 'textarea',
        smallText: 'content of the text',
        name: 'Text content',
        rules: 'required|min:4',
        placeholder: 'Input text, please',
        value: ''
    }
}