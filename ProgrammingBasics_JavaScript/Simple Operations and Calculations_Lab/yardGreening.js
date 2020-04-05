function greening(squareMetters){
    let pricePerSquareMetter = 7.61;
    let discountPercentage = 18;

    let priceWithVAT = squareMetters * pricePerSquareMetter;
    let discount = priceWithVAT*(discountPercentage/100);
    let finalPrice = priceWithVAT -discount;
    
    console.log(`The final price is: ${finalPrice.toFixed(2)} lv.\nThe discount is: ${discount.toFixed(2)} lv.`)
}


// greening(540);
// greening(135);