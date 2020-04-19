function volleyball(typeOfYear, p, h) {
    p = Number(p);
    h = Number(h);
    let weekendsInSofia = 48 - h;
    let playsInSofia = weekendsInSofia * 3 / 4;
    let playsInHomeTown = h;
    let playsInNationalHolliday = p * 2 / 3;
    let totalPlays = playsInHomeTown + playsInSofia + playsInNationalHolliday;

    if (typeOfYear === 'leap') {
        totalPlays += totalPlays * 0.15;
    }

    totalPlays = Math.floor(totalPlays);

    console.log(totalPlays)
}

volleyball('leap', 5, 2)
volleyball('normal', 3, 2)
volleyball('leap', 2, 3)
volleyball('normal', 11, 6)
volleyball('leap', 0, 1)
volleyball('normal', 6, 13)