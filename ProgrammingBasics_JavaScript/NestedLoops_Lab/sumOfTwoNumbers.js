function sumOfTwoNumbers(input) {
    let start = Number(input.shift())
    let end = Number(input.shift());
    let magicNumber = Number(input.shift());
    let counter = 0;
    let isFound = false;
    
    for (let i = start; i <= end; i++) {
        for (let j = start; j <= end; j++) {
            counter++;
            if (i + j === magicNumber) {               
                isFound = true;
                console.log(`Combination N:${counter} (${i} + ${j} = ${magicNumber})`)
                break;
            }           
        }

        if (isFound) break;
    }

    if (!isFound) {
        console.log(`${counter} combinations - neither equals ${magicNumber}`);
    }

}

// sumOfTwoNumbers([1, 10, 5])
// sumOfTwoNumbers([23, 24, 20])