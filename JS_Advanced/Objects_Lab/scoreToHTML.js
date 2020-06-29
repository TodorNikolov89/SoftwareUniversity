function scoreToHTML(input) {
    let text = JSON.parse(input);
    let htmlResult = '<table>\n';

    htmlResult += "  <tr><th>name</th><th>score</th></tr>\n";


    for (const el of text) {
        htmlResult += `  <tr><td>${htmlEscape(el.name)}</td><td>${(el.score)}</td></tr>\n`       
    }

    htmlResult += "</table>"

    console.log(htmlResult);

    function htmlEscape(text) {
        let map = { '"': '&quot;', '&': '&amp;',
            "'": '&#39;', '<': '&lt;', '>': '&gt;' };
        return text.replace(/[\"&'<>]/g, ch => map[ch]);
    }
}

// scoreToHTML([
//     '[{"name":"Pesho","score":479},{ "name": "Gosho", "score": 205 }]'
// ])

scoreToHTML(
    [
        '[{"name":"Pesho & Kiro",    "score":479   },{"name":"Gosho, Maria & Viki","score":205   }]'
    ])