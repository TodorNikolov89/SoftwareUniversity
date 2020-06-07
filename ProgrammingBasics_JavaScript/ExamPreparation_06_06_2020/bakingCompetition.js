function solve(input) {
    let numberOfPlayers = Number(input.shift());
    let totalMoney = 0;
    let totalDeserts = 0;

    for (let i = 0; i < numberOfPlayers; i++) {
        let playerName = input.shift();
        let cookies = 0;
        let cakes = 0;
        let waffles = 0;
        let message = '';

        while (input[0] !== 'Stop baking!') {
            let desert = input.shift();
            let numberOfDeserts = Number(input.shift());
            totalDeserts += numberOfDeserts;
            if (desert === 'cookies') {
                cookies += numberOfDeserts;
            } else if (desert === 'waffles') {
                waffles += numberOfDeserts;
            } else if (desert === 'cakes') {
                cakes += numberOfDeserts;
            }
        }
        input.shift();

        totalMoney += cookies * 1.50 + waffles * 2.30 + cakes * 7.80;
        message = `${playerName} baked ${cookies} cookies, ${cakes} cakes and ${waffles} waffles.`
        console.log(message);
    }
    console.log(`All bakery sold: ${totalDeserts}`)
    console.log(`Total sum for charity: ${totalMoney.toFixed(2)} lv.`)
}

// solve([
//     '3',
//     'Iva',
//     'cookies',
//     '5',
//     'cakes',
//     '3',
//     'Stop baking!',
//     'George',
//     'cakes',
//     '7',
//     'waffles',
//     '2',
//     'Stop baking!',
//     'Ivan',
//     'cookies',
//     '6',
//     'Stop baking!'
// ])

// solve([
// '2',
// 'Annie',
// 'cakes',
// '3',
// 'waffles',
// '6',
// 'cookies',
// '2',
// 'Stop baking!',
// 'Petya',
// 'waffles',
// '8',
// 'Stop baking!'
// ])

solve([
    '3',
    'George',
    'cookies',
    '10',
    'Stop baking!',
    'Annie',
    'waffles',
    '8',
    'Stop baking!',
    'Ivan',
    'cookies',
    '6',
    'waffles',
    '3',
    'Stop baking!'
])