function hotelRoom(month, nights) {
    nights = Number(nights)
    let studioPrice = 0;
    let apartmentPrice = 0;
    let studioMoney = 0;
    let apartmentMoney = 0

    if (month == 'May' || month == 'October') {
        studioPrice = 50.0;
        apartmentPrice = 65.0;

        if (nights > 14) {
            studioPrice *= 0.7;
        } else if (nights > 7) {
            studioPrice *= 0.95;
        }
    } else if (month == 'June' || month == 'September') {
        studioPrice = 75.20;
        apartmentPrice = 68.70;

        if (nights > 14) {
            studioPrice *= 0.8;
        }
       
    } else if (month == 'July' || month == 'August') {
        studioPrice = 76.0;
        apartmentPrice = 77.0;        
    }

    if (nights > 14) {
        apartmentPrice *= 0.90;
    }

    studioMoney = studioPrice * nights;

    apartmentMoney = apartmentPrice * nights;

    console.log(`Apartment: ${apartmentMoney.toFixed(2)} lv.`)
    console.log(`Studio: ${studioMoney.toFixed(2)} lv.`)
}

// hotelRoom('August', 20)
// hotelRoom("May", 15)
// hotelRoom('June', 14)