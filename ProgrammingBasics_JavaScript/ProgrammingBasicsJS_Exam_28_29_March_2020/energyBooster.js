function energyBooster(input) {
    let fruit = input.shift();
    let size = input.shift();
    let amount = Number(input.shift());
    let price = 0;
    let discount = 0;

    switch (size) {
        case 'small':
            if (fruit === 'Watermelon') {
                price = 56 * 2 * amount;
            } else if (fruit === 'Mango') {
                price = 36.66 * 2 * amount;
            }
            else if (fruit === 'Pineapple') {
                price = 42.10 * 2 * amount;
            }
            else if (fruit === 'Raspberry') {
                price = 20 * 2 * amount;
            }
            break;
        case 'big':
            if (fruit === 'Watermelon') {
                price = 28.70 * 5 * amount;
            } else if (fruit === 'Mango') {
                price = 19.60 * 5 * amount;
            }
            else if (fruit === 'Pineapple') {
                price = 24.80 * 5 * amount;
            }
            else if (fruit === 'Raspberry') {
                price = 15.20 * 5 * amount;
            }
            break;
    }

    if (price >= 400 && price <= 1000) {
        discount = 0.15;
    } else if (price > 1000) {
        discount = 0.5;
    }


    price -= price * discount;

    console.log(`${price.toFixed(2)} lv.`)

}

// energyBooster(['Watermelon', 'big', 4])