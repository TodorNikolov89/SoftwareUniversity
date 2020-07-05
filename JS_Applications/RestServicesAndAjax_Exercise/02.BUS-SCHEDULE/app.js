function solve() {
    let arriveBtn = document.getElementById('arrive');
    let departBtn = document.getElementById('depart');
    let baseUrl = `https://judgetests.firebaseio.com/schedule/`;
    let infoBox = document.querySelector('span.info');
    let busStopId = 'depot';
    let busStopName;

    function depart() {
        fetch(baseUrl + `${busStopId}.json`)
            .then((res) => res.json())
            .then((res) => updateData(res))
            .catch();

        function updateData(data) {
            infoBox.textContent = `Next stop ${data.name}`;
            busStopId = data.next
            busStopName = data.name
            departBtn.disabled = true;
            arriveBtn.disabled = false;
        }
    }

    function arrive() {
        infoBox.textContent = `Arriving at ${busStopName}`
        arriveBtn.disabled = true;
        departBtn.disabled = false;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();