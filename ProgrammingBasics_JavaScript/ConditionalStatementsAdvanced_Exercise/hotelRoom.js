function hotelRoom(month, nights) {
    nights = Number(nights)
    let studioPrice = 0;
    let apartmentPrice = 0;
    let studioDiscount = 0;
    let apartmentDiscount = 0;
    let studioMoney = 0;
    let apartmentMoney = 0

    if (month === 'May' || month === 'Octomber') {
        studioPrice = 50;
        apartmentPrice = 65;

        if (nights > 14) {
            studioPrice *= 1-0.3;
        } else if (nights > 7) {
            studioPrice *= 1-0.05;
        }
    } else if (month === 'June' || month === 'September') {
        studioPrice = 75.20;
        apartmentPrice = 68.70;

        if (nights > 14) {
            studioPrice *= 1-0.2;
        }
       
    } else if (month === 'July' || month === 'August') {
        studioPrice = 76;
        apartmentPrice = 77;
    }

    if (nights > 14) {
        apartmentPrice *=1-0.1;
    }

    studioMoney = studioPrice * nights;

    apartmentMoney = apartmentPrice * nights;

    console.log(`Apartment: ${apartmentMoney.toFixed(2)} lv.\nStudio: ${studioMoney.toFixed(2)} lv.`)
}

// hotelRoom('August', 20)
// hotelRoom("May", 15)
// hotelRoom('June', 14)