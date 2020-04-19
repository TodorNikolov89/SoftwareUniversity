function minNumber(input){
    let n = Number(input.shift());
    let number = Number(input.shift());
    let minNumber = number;
    for (let i = 0; i < n; i++) {
        number = Number(input.shift());

        if(number < minNumber){
            minNumber = number;
        }
    }

    console.log(minNumber)
}