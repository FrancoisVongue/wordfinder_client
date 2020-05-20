const {Translate} = require('@google-cloud/translate').v2;

// Creates a client
const translate = new Translate();

/**
 * TODO(developer): Uncomment the following lines before running the sample.
 */
const text = ['Hello', 'world']; // text can be string or an array of strings
const target = 'ru';

async function translateText() {
  let [translations] = await translate.translate(text, target);
  console.log('Translations:');
  translations.forEach((translation, i) => {
    console.log(`${text[i]} => (${target}) ${translation}`);
  });
}

translateText();