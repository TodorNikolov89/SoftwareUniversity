function specialNumbers(start, end, n) {
    start = Number(start);
    end = Number(end);
    n = Number(n);
    let outputMessage = '';

    for (let i = start; i <= end; i++) {
        if (i % n === 0) {
            outputMessage = `Special number: ${i}`;

            if (i % Math.pow(n, 2) === 0) {
                outputMessage = `Very special number: ${i}`;
            }
        } else {
            outputMessage = i;
        }

        console.log(outputMessage);        
    }
}

// specialNumbers(1, 25, 3)
// specialNumbers(1, 10, 4)