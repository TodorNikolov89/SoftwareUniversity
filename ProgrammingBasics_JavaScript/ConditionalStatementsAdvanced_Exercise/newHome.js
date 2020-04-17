function newHome(flowers, flowersCount, budged) {
    flowersCount = Number(flowersCount);
    budged = Number(budged);
    let discount = 0;
    let increase = 0;
    let outputMessage = ""
    let totalmoney = 0;
    let difference = 0;

    switch (flowers) {
        case "Roses":
            if (flowersCount > 80) {
                discount = 10;
            }

            totalmoney = flowersCount * 5;
            totalmoney -= totalmoney * discount/100;
            break;
        case "Dahlias":
            if (flowersCount > 90) {
                discount = 15;
            }
            totalmoney = flowersCount * 3.80;
            totalmoney -= totalmoney * discount/100;
            break;
        case "Tulips":
            if (flowersCount > 80) {
                discount = 15;
            }
            totalmoney = flowersCount * 2.80;
            totalmoney -= totalmoney * discount/100;
            break;
        case "Narcissus":
            if (flowersCount < 120) {
                increase = 15;
            }
            totalmoney = flowersCount * 3;
            totalmoney += totalmoney * increase/100;
            break;
        case "Gladiolus":
            if (flowersCount < 80) {
                increase = 20;
            }
            totalmoney = flowersCount * 2.50;
            totalmoney += totalmoney * increase/100;
            break;
    }    

    if(budged >= totalmoney){
         difference = budged - totalmoney;
        outputMessage = `Hey, you have a great garden with ${flowersCount} ${flowers} and ${difference.toFixed(2)} leva left.`;
    } else {
        difference = totalmoney - budged;
        outputMessage = `Not enough money, you need ${difference.toFixed(2)} leva more.`;
    }

    console.log(outputMessage);
}

//newHome('Roses', 55, 250)
// newHome('Tulips', 88, 260)