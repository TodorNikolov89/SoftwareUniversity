function solve(input) {
    let cruiseType = input.shift();
    let roomType = input.shift();
    let nigths = Number(input.shift());
    let totalPrice = 0;


    switch (cruiseType) {
        case 'Mediterranean':
            if (roomType === 'standard cabin') {
                totalPrice = 4 * 27.50 * nigths
            } else if (roomType === 'cabin with balcony') {
                totalPrice = 4 * 30.20 * nigths
            } else if (roomType === 'apartment') {
                totalPrice = 4 * 40.50 * nigths
            }
            break;
        case 'Adriatic':
            if (roomType === 'standard cabin') {
                totalPrice = 4 * 22.99 * nigths
            } else if (roomType === 'cabin with balcony') {
                totalPrice = 4 * 25.00 * nigths
            } else if (roomType === 'apartment') {
                totalPrice = 4 * 34.99 * nigths
            }
            break;
        case 'Aegean':
            if (roomType === 'standard cabin') {
                totalPrice = 4 * 23.00 * nigths
            } else if (roomType === 'cabin with balcony') {
                totalPrice = 4 * 26.60 * nigths
            } else if (roomType === 'apartment') {
                totalPrice = 4 * 39.80 * nigths
            }
            break;
    }


    if (nigths > 7) {
        totalPrice -= totalPrice * 0.25;
    }


    console.log(`Annie's holiday in the ${cruiseType} sea costs ${totalPrice.toFixed(2)} lv.`)
}

solve(['Adriatic', 'apartment', '5'])
solve(['Aegean', 'standard cabin', '10'])