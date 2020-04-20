function vacation(input) {
    let tripMoney = Number(input.shift());
    let availableMoney = Number(input.shift());
    let action = input.shift();
    let money = Number(input.shift());
    let days = 0;
    let spendingCounter = 0;
    let outputMessage = '';
    let isSpendeverything = false;

    while (availableMoney < tripMoney) {
        days++;

        switch (action) {
            case 'spend':
                availableMoney -= money;
                availableMoney = availableMoney <0 ? 0 : availableMoney
                spendingCounter++;
                break;
            case 'save':
                availableMoney += money;
                spendingCounter = 0;
                break;
        }

        if (spendingCounter === 5) {
            outputMessage = `You can't save the money.\n${days}`;
            isSpendeverything = true;
            break;
        }

        action = input.shift();
        money = Number(input.shift());
    }

    if (isSpendeverything) {
        console.log(outputMessage)
    } else {
        console.log(`You saved the money for ${days} days.`)
    }
}


// vacation([2000, 1000, "spend", 1200, "save", 2000])