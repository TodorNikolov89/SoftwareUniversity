function sumByTown(input) {
    let obj = {};
    for (let i = 0; i < input.length; i += 2) {

        let key = input[i];
        let value = input[i + 1]
        if (!obj.hasOwnProperty(key)) {
            obj[key] = 0;
        }

        obj[key] += Number(value);

    }
    let res = JSON.stringify(obj);
    console.log(res);
}


sumByTown([
    'Sofia',
    '20',
    'Varna',
    '3',
    'sofia',
    '5',
    'varna',
    '4'
])