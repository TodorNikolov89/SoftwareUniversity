function vowelsSum(input) {
    let result = 0;
    for (let ch of input) {
        if (ch === 'a') {
            result += 1;
        } else if (ch === 'e') {
            result += 2;
        } else if (ch === 'i') {
            result += 3;
        } else if (ch === 'o') {
            result += 4;
        } else if (ch === 'u') {
            result += 5;
        }
    }

    console.log(result)
}

vowelsSum('bamboo')