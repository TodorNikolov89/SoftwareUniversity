function coffeeMachine(input) {
    let money = 0;
    let drink;
    let typeOfCoffee;
    let price = 0;
    let sugarQuantity = 0;
    let income = 0;

    for (let iterator of input) {

        let arr = iterator.split(", ");
        money = Number(arr.shift());
        drink = arr.shift();
        if (drink === 'coffee') {
            typeOfCoffee = arr.shift();
            if (typeOfCoffee === 'caffeine') {
                price = 0.80;
            } else if (typeOfCoffee === 'decaf') {
                price = 0.90;
            }
        } else if (drink === 'tea') {
            price = 0.80;
        }

        if (arr[0] === 'milk') {
            arr.shift();
            price += Number((price * 0.10).toFixed(1));
        }

        sugarQuantity = Number(arr.shift());
        if (sugarQuantity > 0 && sugarQuantity <= 5) {
            price += 0.10;
        }
        let outputString = '';
        if (money >= price) {
            let change = money - price;
            outputString = `You ordered ${drink}. Price: $${price.toFixed(2)} Change: $${change.toFixed(2)}`;
            income += price
        } else {
            let neededMoney = price - money;
            outputString = `Not enough money for ${drink}. Need $${neededMoney.toFixed(2)} more`;
        }

        console.log(outputString)
        
    }
    console.log(`Income Report: $${income.toFixed(2)}`)

}

coffeeMachine(['8.00, coffee, decaf, 4', '1.00, tea, 2'])