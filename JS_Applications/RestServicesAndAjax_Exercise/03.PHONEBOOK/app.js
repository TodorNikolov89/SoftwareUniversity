function attachEvents() {
    const elements = {
        person() { return document.querySelector('input#person') },
        phone() { return document.querySelector('input#phone') },
        createContact() { return document.querySelector('button#btnCreate') },
        load() { return document.querySelector('button#btnLoad') },
        phonebook() { return document.querySelector('ul#phonebook') }
    };

    const baseUrl = 'http://localhost:8000/phonebook';

    elements.createContact().addEventListener('click', () => {
        const { value: person } = elements.person();
        const { value: phone } = elements.phone();

        fetch(baseUrl, {
            method: "POST",
            body: JSON.stringify({ person, phone })
        })
            .then((responce) => responce.json())

    })

    elements.load().addEventListener('click', () => {
        fetch(baseUrl)
            .then((response) => response.json())
            .then((contacts) => {
                elements.phonebook().innerHTML = ''
                console.log(typeof contacts)
                Array.from(contacts).forEach(c => {
                    console.log(c)
                })
                for (const [key, prop] of Object.keys(contacts)) {
                    console.log(key)
                    console.log(prop)
                    // const key = c
                    // let listItem = document.createElement('li')
                    // listItem.textContent = `${contacts[key].person} - ${contacts[key].phone}`;
                    // let deleteBtn = document.createElement('button')
                    // deleteBtn.textContent = "Delete";
                    // deleteBtn.addEventListener('click', () => {
                    //     let index = Array.from(contacts).indexOf(Array.from(contacts)[key])
                    //     contacts.splice(index, 1)
                    //     listItem.remove();
                    //     console.log(contacts)
                    // })
                    // listItem.appendChild(deleteBtn)
                    // elements.phonebook().appendChild(listItem)
                }
            })
        // .catch((error) => console.error(error))
    })
}

attachEvents();