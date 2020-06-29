function aggregateElements(input) {
    let sum = input.reduce((a, b) => a + b, 0)
    let secSum = 0;
    let concatSum = '';


    for (let num of input) {
        secSum += 1 / num;
    }

    for (let num of input) {
        concatSum += num.toString();
    }

    console.log(sum);
    console.log(secSum);
    console.log(concatSum);
}

// aggregateElements([1, 2, 3])
// aggregateElements([2, 4, 8, 16])