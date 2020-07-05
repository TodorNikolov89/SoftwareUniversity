function getInfo() {
    let baseUrl = `https://judgetests.firebaseio.com/businfo/{stopId}.json`
    let validIDs = ['1287', '1308', '1327', '2334'];
    const elements = {
        stopId() { return document.querySelector('input#stopId') },
        stopName() { return document.querySelector('div#stopName') },
        buses() { return document.querySelector('ul#buses') }
    }

    const stopId = elements.stopId().value;

    if (!validIDs.includes(stopId)) {
        elements.stopName().textContent = `ERROR`
        return;
    }

    let url = baseUrl.replace("{stopId}", stopId)
    fetch(url)
        .then((res) => res.json())
        .then((result) => showInfo(result))
        .catch()


    function showInfo(data) {
        elements.stopName().textContent = data.name;

        Object.keys(data.buses).forEach((bus) => {
            let listItem = document.createElement('li');
            listItem.textContent = `Bus ${bus} arrices in ${data.buses[bus]}`;
            elements.buses().appendChild(listItem);
        })
    }
}