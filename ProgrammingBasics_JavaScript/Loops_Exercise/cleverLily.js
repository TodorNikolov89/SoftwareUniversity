function cleverLily(age, washingPrice, toyPrice) {
    age = Number(age);
    washingPrice = Number(washingPrice);
    toyPrice = Number(toyPrice);
    let birthdayMoney = 0;
    let toys = 0;
    let toysMoney = 0;
    let outputMessage = '';
    let money = 0;
    let counter = 0;

    for (let i = 1; i <= age; i++) {
        if (i % 2 === 0) {
            counter++;
            money += 10;
            birthdayMoney += money;
        } else {
            toys++;
        }
    }

    toysMoney = toys * toyPrice;
    birthdayMoney += toysMoney;
    birthdayMoney -= counter;
    let difference = Math.abs(birthdayMoney - washingPrice);

    if (birthdayMoney >= washingPrice) {
        outputMessage = `Yes! ${difference.toFixed(2)}`;
    } else {
        outputMessage = `No! ${difference.toFixed(2)}`
    }

    console.log(outputMessage);
}

// cleverLily(10, 170.00, 6)