function journey(budged, season) {
    budged = Number(budged);
    let destination = "";
    let place = ""
    let price = 0;
    if (budged <= 100) {
        destination = "Bulgaria";
        if (season === 'winter') {
            place = 'Hotel';
            price = budged * 0.70;
        } else if (season === 'summer') {
            place = 'Camp';
            price = budged * 0.30;
        }
    } else if (budged <= 1000) {
        destination = "Balkans";
        if (season === 'winter') {
            place = 'Hotel';
            price = budged * 0.80;
        } else if (season === 'summer') {
            place = 'Camp';
            price = budged * 0.40;
        }
    } else {
        destination = "Europe";
        price = budged * 0.90;
        place = 'Hotel';
    }

    console.log(`Somewhere in ${destination}\n${place} - ${price.toFixed(2)}`);
}

// journey(50, 'summer')