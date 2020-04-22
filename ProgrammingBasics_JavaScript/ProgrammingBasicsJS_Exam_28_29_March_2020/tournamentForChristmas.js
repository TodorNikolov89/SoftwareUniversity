function tournamentForChristmas(input) {
    let days = Number(input.shift());
    let balanceWonLose = 0;
    let totalMoney = 0;

    for (let i = 0; i < days; i++) {
        let sport = input.shift();
        let moneyWonPerDay = 0;
        let balanceWonLostGamesPerDay = 0;
        while (sport !== 'Finish') {
            let winOrLose = input.shift();

            if (winOrLose === 'win') {
                balanceWonLostGamesPerDay++;
                balanceWonLose++;
                moneyWonPerDay += 20;
            } else {
                balanceWonLostGamesPerDay--;
                balanceWonLose--;
            }

            sport = input.shift();
        }

        if (balanceWonLostGamesPerDay > 0) {
            moneyWonPerDay += moneyWonPerDay * 0.10;
        }

        totalMoney += moneyWonPerDay;
    }
    let outputMessage = '';

    if (balanceWonLose > 0) {
        totalMoney += totalMoney * 0.20;
        outputMessage = `You won the tournament! Total raised money: ${totalMoney.toFixed(2)}`;
    } else {
        outputMessage = `You lost the tournament! Total raised money: ${totalMoney.toFixed(2)}`;
    }

    console.log(outputMessage);
}

// tournamentForChristmas([2,
//     "volleyball",
//     "win",
//     "football",
//     "lose",
//     "basketball",
//     "win",
//     "Finish",
//     "golf",
//     "win",
//     "tennis",
//     "win",
//     "badminton",
//     "win",
//     "Finish",
// ])

tournamentForChristmas([
    3,
    "darts",
    "lose",
    "handball",
    "lose",
    "judo",
    "win",
    "Finish",
    "snooker",
    "lose",
    "swimming",
    "lose",
    "squash",
    "lose",
    "table tennis",
    "win",
    "Finish",
    "volleyball",
    "win",
    "basketball",
    "win",
    "Finish",

])