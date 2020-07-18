(function () {
    const appEl = document.getElementById('app');

    function init() {
        Promise.all([
            fetch('./contact-card.hbs').then(res => res.text()),
            fetch('./contacts.hbs').then(res => res.text()),
            fetch('./contacts.json').then(res => res.json())
        ])
            .then(([contactCardTemplateString, contactTemplateString, contacts]) => {
                Handlebars.registerPartial('contact', contactCardTemplateString);
                const template = Handlebars.compile(contactTemplateString);
                appEl.innerHTML = template({ contacts })
                let contactsDiv = appEl.querySelector('#contacts');
                contactsDiv.addEventListener('click', function (e) {
                    const target = e.target;
                    if (!target.classList.contains('detailsBtn')) { return; }
                    const detailsEl = target.parentElement.querySelector('.details')
                    if (detailsEl.style.display === 'block') {
                        detailsEl.style.display = 'none';
                    } else {
                        detailsEl.style.display = 'block';
                    }
                })

            })
    }
    init();
}());