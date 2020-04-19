function accoundBalance(input) {
    let n = Number(input.shift());
    let counter = 0;
    let number = Number(input.shift());
    let message = '';
    let sum = 0;

    while (counter < n ) {     
        counter++;
        if (number < 0) {
            message = `Invalid operation!`;
            console.log(message)
            break
        } else {
            message = `Increase: ${number.toFixed(2)}`;       
            sum += number;     
        }

        console.log(message)
        number = Number(input.shift());
    }

    console.log(`Total: ${sum.toFixed(2)}`)
}

// accoundBalance([3, 5.51, 69.42, 100])
// accoundBalance([5, 120, 45.55, -150])