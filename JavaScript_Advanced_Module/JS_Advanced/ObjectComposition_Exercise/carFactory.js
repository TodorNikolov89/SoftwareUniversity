function solve(obj) {
    let car = {
        model: obj.model,
        carriage: {
            type: obj.carriage,
            color: obj.color
        }
    }
    let engines = [{
        power: 90,
        volume: 1800
    }, {
        power: 120,
        volume: 2400
    }, {
        power: 200,
        volume: 3500
    }]

    for (const engine in engines) {
        if (engines[engine].power >= obj.power) {
            car.engine = Object.assign(engines[engine])
            break;
        }
    }
    let wheelSize = 0;
    if (obj.wheelsize % 2 === 1) {
        wheelSize = obj.wheelsize;
    } else {
        wheelSize = obj.wheelsize - 1;
    }

    car.wheels = [wheelSize, wheelSize, wheelSize, wheelSize]
    return car;
}

solve({
    model: 'VW Golf II',
    power: 90,
    color: 'blue',
    carriage: 'hatchback',
    wheelsize: 14
}
)