function solve(intelligence, power, gender) {
    intelligence = Number(intelligence);
    power = Number(power);


    if (intelligence >= 80 && power >= 80 && gender === 'female') {
        console.log('Queen Bee')
    } else if (intelligence >= 80) {
        console.log('Repairing Bee');
    } else if (intelligence >= 60) {
        console.log('Cleaning Bee')
    } else {
        if (power >= 80 && gender === 'male') {
            console.log('Drone Bee');
        } else if (power >= 60) {
            console.log('Guard Bee')
        } else {
            console.log('Worker Bee')
        }
    }
}

solve(82, 81, 'female')
solve(90, 65, 'male')
