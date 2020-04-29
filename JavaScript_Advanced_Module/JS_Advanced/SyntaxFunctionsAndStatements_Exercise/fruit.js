function fruit(fruit, weight, pricePerKg) {
    weight = Number(weight) / 1000;
    pricePerKg = Number(pricePerKg);

    let priceInKg = weight * pricePerKg;
    let outputString = `I need $${priceInKg.toFixed(2)} to buy ${weight.toFixed(2)} kilograms ${fruit}.`;

    console.log(outputString)
}

// fruit('orange', 2500, 1.80)