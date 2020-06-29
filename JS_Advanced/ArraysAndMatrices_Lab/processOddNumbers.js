function processOddNumbers(input) {
    let arr = [];

    for (let i = input.length; i >= 0; i--) {
        if (i % 2 === 1) {
            arr.push(input[i] * 2);
        }
    }

    console.log(arr.join(" "))
}

processOddNumbers([10, 15, 20, 25])