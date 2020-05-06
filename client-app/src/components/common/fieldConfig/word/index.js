export default {
    contentField(word) {
        return {
            id: `word${word.id}_content-input`,
            type: 'text',
            smallText: '',
            name: 'Word',
            rules: 'alpha_dash|required|min:3',
            placeholder: 'Input the word, please',
            value: ''
        }
    },
    tagField(word) {
        return {
            id: `tags${word.id}_input`,
            type: 'text',
            smallText: 'tags to assign',
            name: 'Tags',
            rules: 'CSV',
            placeholder: 'Input tags, please',
            value: ''
        }
    },
    translationField(word) {
        return {
            id: `translations${word.id}_input`,
            type: 'text',
            smallText: `translations`,
            name: 'Translations',
            rules: 'CSV',
            placeholder: 'input translations',
            value: ''
        }
    }
}