export default {
    NameField: {
        id: 'text-name_input',
        type: 'text',
        smallText: '',
        name: 'Text name',
        rules: 'required|min:2',
        placeholder: 'Input the name of the text, plese',
        value: ''
    },
    ContentField: {
        id: 'text-content_input',
        type: 'textarea',
        smallText: 'content of the text',
        name: 'Text content',
        rules: 'required|min:3',
        placeholder: 'Input text, please',
        value: ''
    }
}