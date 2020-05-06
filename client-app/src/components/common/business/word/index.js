export default {
    CSVtoArrayOfObjects(csv, name) {
        const result = csv
            .split(',')
            .filter(t => t.trim())
            .map(t => {return {[name]: t.trim()}});
        return result;
    },
    setFieldValues(word) {
        return {
            content: word.content,
            tags: word.tags
                .map(t => t.name)
                .join(', '),
            translations: word.translations
                .map(t => t.content)
                .join(', ')
        }
    }
}