function autoEngineeringCompany(input) {
    let cars = new Map();
    for (let element of input) {
        let line = element.split(" | ");
        let carBrand = line[0];
        let carModel = line[1];
        let producedCars = Number(line[2]);

        if (!cars.has(carBrand)) {
            let models = new Map();
            models.set(carModel, producedCars);
            cars.set(carBrand, models);
        } else if (!cars.get(carBrand).has(carModel)) {
            cars.get(carBrand).set(carModel, producedCars);
        } else {
            cars.get(carBrand).set(carModel, cars.get(carBrand).get(carModel) + producedCars)
        }
    }

    for (let [car, models] of cars ) {
        console.log(car)
        for (let [model, prodCars] of models) {
            console.log(`###${model} -> ${prodCars}`)
        }
    }
}


autoEngineeringCompany([
    'Audi | Q7 | 1000',
    'Audi | Q6 | 100',
    'BMW | X5 | 1000',
    'BMW | X6 | 100',
    'Citroen | C4 | 123',
    'Volga | GAZ-24 | 1000000',
    'Lada | Niva | 1000000',
    'Lada | Jigula | 1000000',
    'Citroen | C4 | 22',
    'Citroen | C5 | 10'
])