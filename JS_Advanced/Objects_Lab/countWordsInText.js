function countWordsInText([input]) {
    let obj = {};
    let arr = input.split(/\W+/).filter(w => w != "");
    for (const ch of arr) {
        if (obj.hasOwnProperty(ch)) {
            obj[ch]++;
        } else {
            obj[ch] = 1
        }
    }    

    console.log(JSON.stringify(obj))
}


countWordsInText(['JS devs use Node.js for server-side JS.-- JS for devs'])