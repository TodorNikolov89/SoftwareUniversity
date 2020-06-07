function solve(flower, numberOfFlowers, season) {
    numberOfFlowers = Number(numberOfFlowers);
    let totalHoney = 0;

    switch (flower) {
        case 'Sunflower':
            if (season === 'Spring') {
                totalHoney += numberOfFlowers * 10;
            } else if (season === 'Summer') {
                totalHoney += numberOfFlowers * 8;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Autumn') {
                totalHoney += numberOfFlowers * 12;
                totalHoney -= totalHoney * 0.05;
            }
            break;
        case 'Daisy':
            if (season === 'Spring') {
                totalHoney += numberOfFlowers * 12;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Summer') {
                totalHoney += numberOfFlowers * 8;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Autumn') {
                totalHoney += numberOfFlowers * 6;
                totalHoney -= totalHoney * 0.05;
            }
            break;
        case 'Lavender':
            if (season === 'Spring') {
                totalHoney += numberOfFlowers * 12;
            } else if (season === 'Summer') {
                totalHoney += numberOfFlowers * 8;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Autumn') {
                totalHoney += numberOfFlowers * 6;
                totalHoney -= totalHoney * 0.05;
            }
            break;
        case 'Mint':
            if (season === 'Spring') {
                totalHoney += numberOfFlowers * 10;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Summer') {
                totalHoney += numberOfFlowers * 12;
                totalHoney += totalHoney * 0.10;
            } else if (season === 'Autumn') {
                totalHoney += numberOfFlowers * 6;
                totalHoney -= totalHoney * 0.05;
            }
            break;
    }

    console.log(`Total honey harvested: ${totalHoney.toFixed(2)}`)
}

solve('Sunflower', 11, 'Autumn')
solve('Daisy', 15, 'Spring')