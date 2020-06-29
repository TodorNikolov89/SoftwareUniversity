function roadRadar(input) {
    let speed = Number(input.shift());
    let area = input.shift();
    let limit = 0;
    let outputString = '';

    switch (area) {
        case 'motorway':
            limit = 130;
            outputString = checkSpeed(limit, speed);
            break;
        case 'interstate':
            limit = 90;
            outputString = checkSpeed(limit, speed);
            break;
        case 'city':
            limit = 50;
            outputString = checkSpeed(limit, speed);
            break;
        case 'residential':
            limit = 20;
            outputString = checkSpeed(limit, speed);
            break;
    }

    console.log(outputString);

    function checkSpeed(limit, speed) {
        let result = '';
        if (speed - limit > 0 && speed - limit <= 20) {
            result = 'speeding';
        } else if (speed - limit > 20 && speed - limit <= 40) {
            result = 'excessive speeding';
        } else if (speed - limit > 40) {
            result = 'reckless driving';
        }

        return result;
    }
}

roadRadar([200, 'motorway'])