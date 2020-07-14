function attachEvents() {
    // I use my own database in firebase
    //For test, just change dbName with your own db
    let dbName = `softunitnn`
    let baseURL = `https://${dbName}.firebaseio.com/catches`;

    let loadCatchesBtn = document.querySelector('button.load');
    let addCatchBtn = document.querySelector('button.add')
    let addForm = document.querySelectorAll('#addForm input')
    let catches = document.querySelector('#catches');

    loadCatchesBtn.addEventListener('click', loadAllCatches)
    addCatchBtn.addEventListener('click', addCatch)

    //Load all catches
    async function loadAllCatches() {
        fetch(baseURL + '.json')
            .then((response) => response.json())
            .then(response => {
                catches.innerHTML = ''
                if (response != undefined) {
                    Object.entries(response).forEach(([key, data]) => {
                        let updateBtn = el('button', 'Update',
                            {
                                className: 'update',
                                value: 'Update'
                            });
                        updateBtn.addEventListener('click', e => updateCatch(e));
                        let deleteBtn = el('button', 'Delete',
                            {
                                className: 'delete',
                                value: 'Delete'
                            })
                        deleteBtn.addEventListener('click', e => deleteCatch(e))

                        let div = el('div', [
                            el('label', 'Angler'),
                            el('input', '',
                                {
                                    className: 'angler',
                                    type: 'text',
                                    value: data.angler
                                }),
                            el('hr', ''),
                            el('label', 'Weight'),
                            el('input', '',
                                {
                                    className: 'weight',
                                    type: 'number',
                                    value: data.weigth
                                }),
                            el('hr', ''),
                            el('label', 'Species'),
                            el('input', '',
                                {
                                    className: 'species',
                                    type: 'text',
                                    value: data.species
                                }),
                            el('hr', ''),
                            el('label', 'Location'),
                            el('input', '',
                                {
                                    className: 'location',
                                    type: 'text',
                                    value: data.location
                                }),
                            el('hr', ''),
                            el('label', 'Bait'),
                            el('input', '',
                                {
                                    className: 'bait',
                                    type: 'text',
                                    value: data.bait
                                }),
                            el('hr', ''),
                            el('label', 'CaptureTime'),
                            el('input', '',
                                {
                                    className: 'number',
                                    type: 'number',
                                    value: data.captureTime
                                }),
                            el('hr', ''),
                            updateBtn,
                            deleteBtn
                        ], {
                            className: 'catch',
                            "data-id": key
                        })
                        catches.appendChild(div)
                    })
                }
            })
    }

    //Add new catch
    function addCatch() {
        let currentCatch = {
            angler: addForm[0].value,
            weigth: Number(addForm[1].value),
            species: addForm[2].value,
            location: addForm[3].value,
            bait: addForm[4].value,
            captureTime: parseInt(addForm[5].value)
        }
        //Fill the Add form
        if (!currentCatch.angler || !currentCatch.weigth
            || !currentCatch.species || !currentCatch.location
            || !currentCatch.bait || !currentCatch.captureTime) {
            return;
        }

        fetch(baseURL + '.json', {
            method: 'POST',
            body: JSON.stringify(currentCatch)
        })
            .then(loadAllCatches)
            .then(() => {//Clear add form
                addForm[0].value = '',
                    addForm[1].value = '',
                    addForm[2].value = '',
                    addForm[3].value = '',
                    addForm[4].value = '',
                    addForm[5].value = ''
            })
    }

    //Update catch
    async function updateCatch(e) {
        if (e.target.className === 'update') {
            let element = e.target.parentElement;
            let catchId = element['data-id']
            let updatedCatch = await getCatch(element);

            fetch(baseURL + `/${catchId}.json`, {
                method: "PUT",
                body: JSON.stringify(updatedCatch)
            })
                .then(loadAllCatches)

        }

    }

    //Delete catch
    async function deleteCatch(e) {
        let element = e.target.parentElement;
        let catchId = element['data-id']
        let updatedCatch = await getCatch(element);

        fetch(baseURL + `/${catchId}.json`, {
            method: "delete",
            body: JSON.stringify(updatedCatch)
        })
            .then(element.remove())

    }


    //Get DOM element
    async function getCatch(element) {
        let inputs = element.querySelectorAll('input')
        return {
            angler: inputs[0].value,
            weigth: Number(inputs[1].value),
            species: inputs[2].value,
            location: inputs[3].value,
            bait: inputs[4].value,
            captureTime: parseInt(inputs[5].value)
        }
    }

    //Create element
    function el(type, content, attributes) {
        const result = document.createElement(type);

        if (attributes !== undefined) {
            Object.assign(result, attributes)
        }

        if (Array.isArray(content)) {
            content.forEach(append);
        } else {
            append(content)
        }

        function append(node) {
            if (typeof node === 'string' || typeof node === 'number') {
                node = document.createTextNode(node);
            }

            result.appendChild(node)
        }
        return result;
    }
}
attachEvents();

