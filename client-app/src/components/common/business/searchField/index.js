export default {
    checkMatches(input, tokens) {
        return tokens.filter(token => this.subSequence(token, input));
    },
    /* private */
    subSequence(sup, sub) {
        let result = '';
        for(let i = 0, j = 0; i < sup.length && j < sub.length; i++) {
            if(sup[i] == sub[j]) {
                result += sup[i];
                j++;
            }
        }
        return result.length == sub.length;
    }
}