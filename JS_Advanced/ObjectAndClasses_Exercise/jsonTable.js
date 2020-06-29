function jsonTable(input) {
    let output = "<table>\n";
    for (const line of input) {
        let person = JSON.parse(line);
        output += "\t<tr>\n";
        
        for (let element of Object.values(person)) {
            if (typeof element !== 'number') {
                output += `\t\t<td>${htmlEscape(element)}</td>\n`
            } else {
                output += `\t\t<td>${element}</td>\n`
            }

        }
        output += "\t</tr>\n"
    }
    output += "</table>";
    console.log(output);

    function htmlEscape(text) {
        let map = {
            '"': '&quot;', '&': '&amp;',
            "'": '&#39;', '<': '&lt;', '>': '&gt;'
        };
        return text.replace(/[\"&'<>]/g, ch => map[ch]);
    }
}

jsonTable([
    '{"name":"Pesho","position":"Promenliva","salary":100000}',
    '{"name":"Teo","position":"Lecturer","salary":1000}',
    '{"name":"Georgi","position":"Lecturer","salary":1000}'
])