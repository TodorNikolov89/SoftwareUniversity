function negativePositiveNumbers(arr) {
    let newArr = [];

    for (const element of arr) {
        if (element >= 0) {
            newArr.push(element)
        } else {
            newArr.unshift(element)
        }
    }

    newArr.forEach(element => {
        console.log(element)
    });
}

negativePositiveNumbers([7, -2, 8, 9])