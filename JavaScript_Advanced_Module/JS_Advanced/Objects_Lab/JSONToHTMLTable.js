function jsonToHtmlTable(input) {
    let text = JSON.parse(input);
    let htmlResult = '<table>\n';
    htmlResult += '  <tr>';
    Object.keys(text[0]).forEach(k => htmlResult += `<th>${k}</th>`);
    htmlResult += '</tr>\n';


    for (const el of text) {
        htmlResult += '  <tr>';
        Object.keys(el).forEach(k => htmlResult += `<td>${htmlEscape(String(el[k]))}</td>`);
        htmlResult += '</tr>\n';
    }

    htmlResult += "</table>"

    console.log(htmlResult);

    function htmlEscape(text) {
        let map = {
            '"': '&quot;', '&': '&amp;',
            "'": '&#39;', '<': '&lt;', '>': '&gt;'
        };
        return text.replace(/[\"&'<>]/g, ch => map[ch]);
    }
}

// jsonToHtmlTable([
//     '[{"Name":"Tomatoes & Chips","Price":2.35},{"Name":"J&B Chocolate","Price":0.96}]'])

jsonToHtmlTable(['[{"Name":"Pesho <div>-a","Age":20,"City":"Sofia"},{"Name":"Gosho","Age":18,"City":"Plovdiv"},{"Name":"Angel","Age":18,"City":"Veliko Tarnovo"}]']
)