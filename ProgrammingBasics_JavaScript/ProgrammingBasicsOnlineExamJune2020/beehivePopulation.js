function solve(population, years) {
    population = Number(population)
    years = Number(years);


    for (let i = 1; i <= years; i++) {
        population += Math.floor(population / 10) * 2;

        if (i % 5 === 0) {
            population -= Math.floor(population / 50) * 5;
        }
        population -= Math.floor(population / 20) * 2;
    }


    console.log(`Beehive population: ${population}`)
}

solve(100, 6)
solve(1000, 23)