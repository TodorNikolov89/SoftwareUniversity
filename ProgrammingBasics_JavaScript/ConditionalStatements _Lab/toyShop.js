function calculateTrip(tripPrice, puzzles, dolls, bears, minions, trucks){
    let toys = puzzles + dolls + bears + minions + trucks;
    let discount = 0;
    
    let money = puzzles * 2.60 + dolls * 3 + bears * 4.10 + minions * 8.20 + trucks * 2;

    if(toys >=50){
        money = money - money * 0.25;
    }

    money = money - money * 0.10;
    let difference = Math.abs(tripPrice - money)
    if(money>=tripPrice){
        
        console.log(`Yes! ${difference.toFixed(2)} lv left.`)
    } else {
        console.log(`Not enough money! ${difference.toFixed(2)} lv needed.`)
    }

}

// calculateTrip(40.8, 20, 25, 30, 50, 10)
// calculateTrip(320,8,2,5,5,1)