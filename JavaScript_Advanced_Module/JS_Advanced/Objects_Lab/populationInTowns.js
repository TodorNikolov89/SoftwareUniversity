function populationInTowns(input) {
    let townMap = new Map();

    for (const element of input) {
        let town = element.split(" <-> ")[0];
        let population = Number(element.split(" <-> ")[1]);

        if (!townMap.has(town)) {
            townMap.set(town, 0);
        }

        townMap.set(town, townMap.get(town) + population);
    }

    for(let t of townMap){
        console.log(`${t[0]} : ${t[1]}`)
    }
}

populationInTowns([
    'Istanbul <-> 100000',
    'Honk Kong <-> 2100004',
    'Jerusalem <-> 2352344',
    'Mexico City <-> 23401925',
    'Istanbul <-> 1000'
])