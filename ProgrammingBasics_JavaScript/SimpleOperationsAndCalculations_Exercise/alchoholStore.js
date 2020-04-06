function alchoholStore(whiskeyPrice, beerAmount, wineAmount, rakiaAmount, whiskeyAmount){
    let rakiaPrice = whiskeyPrice/2;
    let winePrice = rakiaPrice - rakiaPrice * 0.40;
    let beerPrice = rakiaPrice - rakiaPrice * 0.80;

    let moneyForWhiskey = whiskeyAmount * whiskeyPrice;
    let moneyForRakia = rakiaAmount * rakiaPrice;
    let moneyForBeer = beerAmount * beerPrice;
    let moneyForWine = wineAmount * winePrice;

    let totalMoney = moneyForBeer + moneyForRakia + moneyForWhiskey + moneyForWine;

    console.log(totalMoney.toFixed(2));
}


alchoholStore(50, 10, 3.5, 6.5, 1)