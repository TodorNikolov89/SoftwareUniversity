function solve(input) {
    let points = Number(input.shift());
    let isFinished = false;
    let moves = 0;
    let message = '';

    while (input.length > 0) {
        let area = input.shift();
        let currentPoints = Number(input.shift());
        moves++;
        switch (area) {
            case 'number section':
                points -= currentPoints;
                break;
            case 'double ring':
                points -= 2 * currentPoints;
                break;
            case 'triple ring':
                points -= 3 * currentPoints;
                break;
            case 'bullseye':
                points -= 3 * currentPoints;
                isFinished = true;
                break;

        }
        if (isFinished) {
            message = `Congratulations! You won the game with a bullseye in ${moves} moves!`
            break;
        }

        if (points === 0) {
            message = `Congratulations! You won the game in ${moves} moves!`;
            isFinished = true;
        } else if (points < 0) {
            message = `Sorry, you lost. Score difference: ${Math.abs(points)}.`
            isFinished = true;
        }
        if (isFinished) {
            break;
        }
    }

    console.log(message)
}

solve(['150',
    'double ring',
    '20',
    'triple ring',
    '10',
    'number section',
    '20',
    'triple ring',
    '20'])