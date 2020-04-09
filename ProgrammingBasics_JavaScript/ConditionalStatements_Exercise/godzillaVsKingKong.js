function solve(money,numberOfPersons, moneyForclothesPerPerson){
    let budged = Number(money);
    let people = Number(numberOfPersons);
    let pricePerCloth = Number(moneyForclothesPerPerson);

    let decorMoney = budged * 0.10; 
    let moneyForClothes = people * pricePerCloth;
    let outputMessage = "";

    if(numberOfPersons>150){
        moneyForClothes -= moneyForClothes * 0.10;
    }

    
    let difference = Math.abs(budged - (decorMoney + moneyForClothes));

    if(decorMoney + moneyForClothes >budged){
        
            outputMessage = `Not enough money!\nWingard needs ${difference.toFixed(2)} leva more.`
    }
    else {
            outputMessage = `Action!\nWingard starts filming with ${difference.toFixed(2)} leva left.`
    }

    console.log(outputMessage)
}

// solve(20000, 120, 55.5)
// solve(15437.62, 186, 57.99)
// solve(9587.88, 222,55.68)