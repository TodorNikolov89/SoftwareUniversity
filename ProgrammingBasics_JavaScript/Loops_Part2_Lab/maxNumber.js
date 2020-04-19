function maxNumber(input) {
    let n = Number(input.shift());
    let number = Number(input.shift());
    let maxNumber = number;
    for (let i = 0; i < n; i++) {
        number = Number(input.shift());

        if(number >maxNumber){
            maxNumber = number;
        }
    }

    console.log(maxNumber)
}

maxNumber([2, 100, 99])