function solve(input) {
    let name = input.shift();
    let games = Number(input.shift());
    let tennisgames = 0;
    let volleyballGames = 0;
    let badmintonGames = 0;
    let tennisPoints = 0;
    let volleyballPoints = 0;
    let badmintonPoints = 0;

    for (let i = 0; i < games; i++) {
        let gameName = input.shift();
        let points = Number(input.shift());

        if (gameName === 'volleyball') {
            volleyballGames++;
            volleyballPoints += (points + 0.07 * points);

        } else if (gameName === 'tennis') {
            tennisgames++;
            tennisPoints += (points + 0.05 * points);

        } else if (gameName === 'badminton') {
            badmintonGames++;
            badmintonPoints += (points + 0.02 * points);
        }
    }
    let avVPoints = Math.floor(volleyballPoints / volleyballGames)
    let avTPoints = Math.floor(tennisPoints / tennisgames)
    let avBPoints = Math.floor(badmintonPoints / badmintonGames)
    let totalPoints = Math.floor(badmintonPoints + volleyballPoints + tennisPoints);
    let message = '';
    if (avVPoints >= 75 && avTPoints >= 75 && avBPoints >= 75) {
        message = `Congratulations, ${name}! You won the cruise games with ${totalPoints} points.`
    } else {
        message = `Sorry, ${name}, you lost. Your points are only ${totalPoints}.`;
    }

    console.log(message);
}


solve([
    'Pepi',
    '3',
    'volleyball',
    '78',
    'tennis',
    '98',
    'badminton',
    '105'
])