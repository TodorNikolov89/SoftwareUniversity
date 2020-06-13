function solve(input) {
    let line = input.shift();
    while (line !== 'Midnight') {
        let isCheating = false;
        let name = line;
        let totalPoints = 0;
        for (i = 0; i < 6; i++) {
            let points = Number(input.shift());

            if (points < 0) {
                console.log(`${name} was cheating!`)
                isCheating = true;
                break;
            }
            totalPoints += points;
        }

        if (isCheating) {
            line = input.shift();
            continue
        }
        totalPoints = Math.floor(totalPoints / 600 * 100)
        totalPoints = totalPoints * 0.06

        if (totalPoints >= 5.00) {
            console.log("===================")
            console.log("|   CERTIFICATE   |")
            console.log(`|    ${totalPoints.toFixed(2)}/6.00    |`)
            console.log("===================")
            console.log(`Issued to ${name}`)

        } else {
            if (totalPoints < 3) {
                totalPoints = 2;
            }
            console.log(`${name} - ${totalPoints.toFixed(2)}`)
        }
        line = input.shift();
    }
}

solve([
    'George',
    100,
    100,
    100,
    100,
    40,
    0,
    'John',
    10,
    -1,
    'Peter',
    100,
    100,
    100,
    100,
    100,
    70,
    'Midnight',
])

// solve([
//     'Andy',
//     50,
//     50,
//     20,
//     10,
//     10,
//     0,
//     'Midnight'

// ])