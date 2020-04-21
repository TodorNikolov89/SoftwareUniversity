function travelling(input) {
    let destination = input.shift();
    let budged = Number(input.shift());
    let savedMoney = 0

    while (destination !== 'End') {

        while (savedMoney < budged) {
            savedMoney += Number(input.shift());
        }
        
        savedMoney = 0;
        console.log(`Going to ${destination}!`)

        destination = input.shift();
        budged = Number(input.shift());
    }
}

// travelling([
//     "Greece",
// 1000,
// 200,
// 200,
// 300,
// 100,
// 150,
// 240,
// "Spain",
// 1200,
// 300,
// 500,
// 193,
// 423,
// "End",
// ])