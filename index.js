const fs = require('fs');

fs.readFile('f:/practice/wordfinder/dictionary.txt', "utf8", (err, data) => {
  if(err) throw new Error(err);
  let d = data.toString().split('\n').map(w => w.trim()).filter(w => w.length > 2);
  fs.writeFile('f:/practice/wordfinder/processedDictionary.txt', JSON.stringify(d), () => {});
});