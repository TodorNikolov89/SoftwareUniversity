/* globals Handlebars */
window.addEventListener('load', async () => {
    const mainEl = document.querySelector('.monkeys')

    const list = await (await (fetch('./listMonkeys.hbs'))).text();
    const listTemplate = Handlebars.compile(list);
    Handlebars.registerPartial('monkey', await (await fetch('./monkey.hbs')).text());

    //render html
    const html = listTemplate({ monkeys });
    mainEl.innerHTML = html;


    mainEl.addEventListener('click', onClick)

    function onClick(e) {
        if (e.target.tagName !== 'BUTTON') {
            return;
        }

        let div = e.target.parentElement.querySelector('p');
        if (div.style.display == 'none') {
            div.style.display = 'block'
        } else {
            div.style.display = 'none'
        }
    }
})