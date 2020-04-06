function charityCampaign(days, cheffs, cakes, waffles, pancakes ){
    let cakesMoney = cakes * 45;
    let wafflesMoney = waffles * 5.80;
    let pancakesMoney = pancakes * 3.20;

    let totalMoney = (cakesMoney + wafflesMoney + pancakesMoney)*cheffs;
    totalMoney = totalMoney * days;
    totalMoney = totalMoney - totalMoney/8;

    console.log(totalMoney.toFixed(2))

}


// charityCampaign(20, 8, 14, 30, 16)
// charityCampaign(131, 5, 9, 33, 46)