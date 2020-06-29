function attachEventsListeners() {
    let convertBtn = document.getElementById('convert');
    convertBtn.addEventListener('click', convert)

    let inputDistance = document.getElementById('inputDistance');
    let outputDistance = document.getElementById('outputDistance');

    let convertFrom = document.getElementById('inputUnits');
    let convertTo = document.getElementById('outputUnits');



    let arr = new Set();

    arr = {
        'km': 1000,
        'm': 1,
        'cm': 0.01,
        'mm': 0.001,
        'mi': 1609.34,
        'yrd': 0.9144,
        'ft': 0.3048,
        'in': 0.0254
    }

    function convert() {
        let inputValue = Number(inputDistance.value);
        if (inputValue === '' || inputValue === '') {
            return;
        }

        if (isNaN(inputValue)) {
            return;
        }


        let fromRate = arr[convertFrom.value];
        let targetRate = arr[convertTo.value];
        if (!fromRate || !targetRate) {
            return;
        }

        let result = inputValue * fromRate / targetRate;
        outputDistance.value = result;

    }
}