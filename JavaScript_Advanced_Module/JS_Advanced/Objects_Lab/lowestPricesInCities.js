function lowestPricesInCities(input) {
    let map = new Map();

    for (const element of input) {
        let tokens = element.split(" | ");
        let town = tokens[0];
        let product = tokens[1];
        let price = Number(tokens[2]);

        if (!map.has(product)) {
            map.set(product, new Map())
        }

        map.get(product).set(town, price);
    }

    for (let [product, insideMap] of map) {
        let lowestPrice = Number.POSITIVE_INFINITY;
        let townWithLowestPrice = "";
        
        for (let [town, price] of insideMap) {
            if (price < lowestPrice) {
                lowestPrice = price;
                townWithLowestPrice = town;
            }
        }

        console.log(`${product} -> ${lowestPrice} (${townWithLowestPrice})`);
    }
}


// lowestPricesInCities(['Sample Town | Sample Product | 1000',
//     'Sample Town | Orange | 2',
//     'Sample Town | Peach | 1',
//     'Sofia | Orange | 3',
//     'Sofia | Peach | 2',
//     'New York | Sample Product | 1000.1',
//     'New York | Burger | 10']
// )

lowestPricesInCities([
    'Sofia City | Audi | 100000',
    'Sofia City | BMW | 100000',
    'Sofia City | Mitsubishi | 10000',
    'Sofia City | Mercedes | 10000',
    'Sofia City | NoOffenseToCarLovers | 0',
    'Mexico City | Audi | 1000',
    'Mexico City | BMW | 99999',
    'New York City | Mitsubishi | 10000',
    'New York City | Mitsubishi | 1000',
    'Mexico City | Audi | 100000',
    'Washington City | Mercedes | 1000'

])