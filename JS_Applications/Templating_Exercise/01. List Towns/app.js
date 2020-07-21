/*globals Handlebars*/
window.addEventListener('load', () => {
    const templateString = document.getElementById('main-template').innerHTML;
    Handlebars.registerPartial('town', document.getElementById('town-template').innerHTML);

    const templateFn = Handlebars.compile(templateString);

    document.querySelector('#btnLoadTowns').addEventListener('click', renderTowns);

    const input = document.querySelector('#towns');
    const rootEl = document.querySelector('#root');

    function renderTowns(e) {
        e.preventDefault();
        const towns = input.value.split(', ');
        const generatedHtml = templateFn({ towns });
        rootEl.innerHTML = generatedHtml;
    }
})