function equalSumsEvenOddsPossition(input) {
    let firstNumber = Number(input.shift());
    let secondNumber = Number(input.shift());
    let outputMessage = "";
    let evenSum = 0;
    let oddSum = 0;

    for (let i = firstNumber; i <= secondNumber; i++) {
        let str = i.toString();

        for (let j = 0; j < str.length; j += 2) {
            oddSum += Number(str[j]);
        }

        for (let k = 1; k < str.length; k += 2) {
            evenSum += Number(str[k]);
        }

        if (oddSum === evenSum) {
            outputMessage += str + " ";
        }

        evenSum = 0;
        oddSum = 0;
    }

    console.log(outputMessage);
}


// equalSumsEvenOddsPossition([100000, 100050])