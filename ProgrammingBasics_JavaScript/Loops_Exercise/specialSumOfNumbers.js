function specialSumOfNumbers(start, end, devider) {
    start = Number(start);
    end = Number(end);
    devider = Number(devider);

    let result = 0;

    for (let i = start; i <= end; i++) {
        if(i % devider === 0){
            result += i;
        }
    }

    console.log(result);
}


// specialSumOfNumbers(10, 30, 7)
// specialSumOfNumbers(61,125,25)