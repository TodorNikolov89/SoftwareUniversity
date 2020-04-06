function calculateSpendings(dogsNumber, leftAnimals){
    let singleDogFoodPrice = 2.50;
    let otherAnimalFoodPrice = 4.00;

    let dogsFoodMoney = singleDogFoodPrice * dogsNumber;
    let leftAnimalFoodMoney = otherAnimalFoodPrice * leftAnimals;

    let totalMoney = dogsFoodMoney + leftAnimalFoodMoney;

    console.log(`${totalMoney.toFixed(2)} lv.`)
}

// calculateSpendings(5, 4);
// calculateSpendings(13,9);