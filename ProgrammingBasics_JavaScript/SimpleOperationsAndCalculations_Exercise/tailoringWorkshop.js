function calculatePrice(numberOfTables, singleTableWidth, singleTableHeight){ 
    let tables = Number(numberOfTables);
    let tableWidth = Number(singleTableWidth)
    let tableHeight = Number(singleTableHeight)

    let priceForTableCloths = (tableWidth + 0.30 * 2) * (tableHeight + 0.30 * 2) * tables;
    let priceForKare = (tableWidth / 2) * (tableWidth / 2) * tables;

    let totalPriceInUsd = priceForKare * 9 + priceForTableCloths * 7
    let totalPriceInBgn = totalPriceInUsd * 1.85

    let result1 = totalPriceInUsd.toFixed(2);
    let result2 = totalPriceInBgn.toFixed(2);

    console.log(result1 + " USD");
    console.log(result2 + " BGN");    
}

// calculatePrice(5, 1.00,0.50)
// calculatePrice(10,1.20,0.65)
