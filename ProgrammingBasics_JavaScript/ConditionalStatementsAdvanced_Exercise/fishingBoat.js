function fishingBoat(budged, season, fishermenCount) {
    budged = Number(budged)
    fishermenCount = Number(fishermenCount)
    let boatRent = 0;
    let discount = 0;
    let neededMoney = 0;
    let difference = 0;
    let outputMessage = "";

    if (fishermenCount <= 6 && fishermenCount > 0) {
        discount = 10;
    } else if (fishermenCount >= 7 && fishermenCount <= 11) {
        discount = 15;
    } else {
        discount = 25;
    }    

    switch (season) {
        case 'Spring':
            boatRent = 3000;
            neededMoney += boatRent - boatRent * discount / 100;
            break;
        case 'Summer':
        case 'Autumn':
            boatRent = 4200;
            neededMoney += boatRent - boatRent * discount / 100;
            break;
        case 'Winter':
            boatRent = 2600;
            neededMoney += boatRent - boatRent * discount / 100;
            break;
    }
  
    if (fishermenCount % 2 === 0 && season != 'Autumn') {
        discount += 5;
        neededMoney -= neededMoney * 0.05;
    }

    if(neededMoney <= budged){
        difference = budged - neededMoney;
        outputMessage = `Yes! You have ${difference.toFixed(2)} leva left.`;
    } else {
        difference = neededMoney - budged;
        outputMessage = `Not enough money! You need ${difference.toFixed(2)} leva.`;
    }

    console.log(outputMessage)
}

// fishingBoat(3000, 'Summer', 11)
// fishingBoat(3000, 'Summer', 4)
// fishingBoat(3600, 'Autumn', 4)
// fishingBoat(3600, 'Autumn', 7)
// fishingBoat(2000, 'Winter', 4)