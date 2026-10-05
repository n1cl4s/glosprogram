
let words = [
  { wordIn: "hem", wordOut: "home" },
  { wordIn: "hus", wordOut: "house" },
  { wordIn: "stor", wordOut: "large" },
  { wordIn: "stor", wordOut: "big" }
];

//let dict = {};

for (let word of words) {
  dict[word.wordIn] = word;
}

/* dict structure
{
  "hem": { wordIn: "hem", wordOut: "home" }
  "hus": { wordIn: "hus", wordOut: "house" } 
  "stor": ['
      { wordIn: "stor", wordOut: "large" },
      { wordIn: "stor", wordOut: "big" }
  ]
}

*/


let dict = {};

for (let word of words) {

  if (!dict[word.wordIn]) {
    dict[word.wordIn] = [];
  }
  dict[word.wordIn].push(word);
}

/* dict structure
{
  "hem": [{ wordIn: "hem", wordOut: "home" }],
  "hus": [{ wordIn: "hus", wordOut: "house" }], 
  "stor": ['
      { wordIn: "stor", wordOut: "large" },
      { wordIn: "stor", wordOut: "big" }
  ]
}
*/