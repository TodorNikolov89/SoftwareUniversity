function cityMarkets(input) {
    let towns = new Map();

    for (let currentTown of input) {
        let tokens = currentTown.split(" -> ");
        let town = tokens[0];
        let product = tokens[1];
        let amountOfSales = Number(tokens[2].split(" : ")[0])
        let priceForOneUnit = Number(tokens[2].split(" : ")[1])
        let totalPricePerProduct = amountOfSales * priceForOneUnit;

        if (!towns.has(town)) {
            let products = new Map();
            products.set(product, totalPricePerProduct);
            towns.set(town, products);
        } else if (!towns.get(town).has(product)) {
            towns.get(town).set(product, totalPricePerProduct)
        } else {
            towns.get(town).set(product, towns.get(town).get(product) + totalPricePerProduct);
        }

    }

    for (let [town, product] of towns) {
        console.log(`Town - ${town}`);

        for (let [product, income] of towns.get(town)) {
            console.log(`$$$${product} : ${income}`);
        }
    }

}


cityMarkets([
    'Sofia -> Laptops HP -> 200 : 2000',
    'Sofia -> Raspberry -> 200000 : 1500',
    'Sofia -> Audi Q7 -> 200 : 100000',
    'Montana -> Portokals -> 200000 : 1',
    'Montana -> Qgodas -> 20000 : 0.2',
    'Montana -> Chereshas -> 1000 : 0.3'
])