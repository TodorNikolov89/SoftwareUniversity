function lotaryStatistics(input) {
    let oddOneDigitNumberCount = 0;
    let evenNumbersCount = 0;
    let oddNumbersEndingOnSeven = 0;
    let numbersDevided100Without = 0;

    for (let i = 1; i <= input; i++) {
        if (i < 10 && i % 2 === 1) {
            oddOneDigitNumberCount++;
        }
        if (i % 2 === 0) {
            evenNumbersCount++;
        }
        if (i % 10 === 7) {
            oddNumbersEndingOnSeven++;
        }
        if (100 % i === 0) {
            numbersDevided100Without++;
        }
    }

    let percentOddOneDigitNumberCount =  oddOneDigitNumberCount / input * 100;  
    let percentEvenNumbersCount =  evenNumbersCount / input * 100;  
    let percentOddNumbersEndingOnSeven  =  oddNumbersEndingOnSeven / input * 100;  
    let percentNumbersDevided100Without = numbersDevided100Without / input * 100;
    
    console.log(`${percentOddOneDigitNumberCount.toFixed(2)}%`)
    console.log(`${percentEvenNumbersCount.toFixed(2)}%`)
    console.log(`${percentOddNumbersEndingOnSeven.toFixed(2)}%`)
    console.log(`${percentNumbersDevided100Without.toFixed(2)}%`)
}


// lotaryStatistics(49);
// lotaryStatistics(35);