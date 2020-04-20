function combinations(input) {
    input = Number(input);
    let counter = 0;
    for (let i = 0; i <= input; i++) {
        for (let j = 0; j <= input; j++) {
            for (let k = 0; k <= input; k++) {
                if (i + j + k === input) {
                    counter++;
                }
            }
        }
    }

    console.log(counter);
}

// combinations(25)