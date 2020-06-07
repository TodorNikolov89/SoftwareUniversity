function solve(input) {
    let honeyAmount = Number(input.shift());
    let honey = 0;
    let line = input.shift();

    while (line !== 'Winter has come') {
        let beeName = line;
        let collectedHoney = 0;

        for (let i = 0; i < 6; i++) {
            collectedHoney += Number(input.shift());
        }

        if (collectedHoney < 0) {
            console.log(`${beeName} was banished for gluttony`);
        }
        honey += collectedHoney;
        if (honey >= honeyAmount) {
            break;
        }
        line = input.shift();
    }

    let difference = Math.abs(honeyAmount - honey);
    if (honeyAmount <= honey) {
        console.log(`Well done! Honey surplus ${difference.toFixed(2)}.`)
    } else {
        console.log(`Hard Winter! Honey needed ${difference.toFixed(2)}.`)
    }
}

// solve([
//     1000,
//     'Maya',
//     50,
//     10.5,
//     19.5,
//     30,
//     100,
//     100,
//     'Winter has come'
// ])

// solve([
//     300,
//     'Beeatrice',
//     50,
//     -10,
//     40,
//     30,
//     100,
//     100,
//     'Winter has come'
// ])

solve([
    1000,
    'Maya',
    -50,
    -10,
    -20.70,
    20.40,
    10.30,
    40,
    'Yama',
    50,
    10,
    20,
    30,
    100,
    100,
    'Winter has come'
])