// export default {
//     checkMatches(input, tokens) {
//         return tokens.filter(token => this.subSequence(token, input));
//     },
//     /* private */
//     subSequence(sup, sub) {
//         let result = '';
//         for(let i = 0, j = 0; i < sup.length && j < sub.length; i++) {
//             if(sup[i] == sub[j]) {
//                 result += sup[i];
//                 j++;
//             }
//         }
//         return result.length == sub.length;
//     },
// }

function editDistance(s1, s2) {
    
    let table = [...Array(s1.length + 1).keys()]
        .map((rv, ri) => [...Array(s2.length + 1)]
        .map((v, i) => i + ri));
    
    let distanceVector = table.reduce((br, r, ri) => 
        r.reduce((b, v, i) => {
            if(ri && i)
            {
                const top = br[i];
                const topleft = br[i-1];
                const left = b[i-1];
                
                let value;
                
                if(s1[ri-1] == s2[i-1])
                    value = Math.min(left, topleft, top);
                else 
                    value = Math.min(top + 1, topleft + 1.5, left + 1)
                
                b[i] = value;
                return b;
                
            } else return b;
        }, r), table[0]);
        
    return distanceVector[s2.length];
}
//editDistance("star wars perform", "mar wards form");
const {performance} = require('perf_hooks');
let start = performance.now();
console.log(editDistance("farm", "form"));
console.log('Duration : ' + (performance.now() - start));