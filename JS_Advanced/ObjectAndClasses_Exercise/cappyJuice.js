function cappyJuice(input) {
    let quantities = {};
    let bottles = {};

    for (const element of input) {
        let line = element.split(" => ");
        let product = line[0];
        let quantity = Number(line[1]);

        if (!quantities.hasOwnProperty(product)) {
            quantities[product] = quantity;
        } else {
            quantities[product] += quantity;
        }

        if (quantities[product] >= 1000) {
            bottles[product] = parseInt(quantities[product] / 1000)
        }
    }

    for (const key of Object.keys(bottles)) {
        console.log(`${key} => ${bottles[key]}`)
    }
}


cappyJuice([
    'Kiwi => 234',
    'Pear => 2345',
    'Watermelon => 3456',
    'Kiwi => 4567',
    'Pear => 5678',
    'Watermelon => 6789'
])