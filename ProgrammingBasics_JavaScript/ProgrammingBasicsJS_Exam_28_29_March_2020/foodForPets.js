function foodForPets(input) {
    let days = Number(input.shift());
    let totalAmountFood = Number(input.shift());
    let eatenFood = 0;
    let foodPerdayForDog = 0;
    let foodPerdayForCat = 0;
    let biscuitsAmount = 0;
    let dogFood = 0;
    let catfood = 0;

    for (let i = 1; i <= days; i++) {
        foodPerdayForDog = Number(input.shift());
        foodPerdayForCat = Number(input.shift());
        dogFood += foodPerdayForDog;
        catfood += foodPerdayForCat;
        eatenFood += foodPerdayForDog + foodPerdayForCat;

        if (i % 3 === 0) {
            biscuitsAmount += (foodPerdayForDog + foodPerdayForCat) * 0.10;
        }

    }

    console.log(`Total eaten biscuits: ${Math.round(biscuitsAmount)}gr.`);
    console.log(`${(eatenFood / totalAmountFood * 100).toFixed(2)}% of the food has been eaten.`)
    console.log(`${(dogFood / eatenFood * 100).toFixed(2)}% eaten from the dog.`)
    console.log(`${(catfood / eatenFood * 100).toFixed(2)}% eaten from the cat.`)

}

// foodForPets([3, 1000, 300, 20, 100, 30, 110, 40])