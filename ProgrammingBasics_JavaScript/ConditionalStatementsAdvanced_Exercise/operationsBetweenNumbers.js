function operationsBetweenNumbers(n1, n2, operation) {
    n1 = Number(n1);
    n2 = Number(n2);
    let result = 0;
    let outputMessage = '';
    let evenOrOdd = '';
    if (operation === '-') {
        result = n1 - n2;
        outputMessage = `${n1} ${operation} ${n2} = ${result} - ${result % 2 === 0 ? "even" : 'odd'}`;
    } else if (operation === '+') {
        result = n1 + n2;
        outputMessage = `${n1} ${operation} ${n2} = ${result} - ${result % 2 === 0 ? "even" : 'odd'}`;
    } else if (operation === '*') {
        result = n1 * n2;
        outputMessage = `${n1} ${operation} ${n2} = ${result} - ${result % 2 === 0 ? "even" : 'odd'}`;
    } else if (operation === '/') {
        if (n2 === 0) {
            outputMessage = `Cannot divide ${n1} by zero`;
        } else {
            result = n1 / n2;
            outputMessage = `${n1} ${operation} ${n2} = ${result.toFixed(2)}`;
        }
    } else if (operation === '%') {
        if (n2 === 0) {
            outputMessage = `Cannot divide ${n1} by zero`;
        } else {
            result = n1 % n2;
            outputMessage = `${n1} ${operation} ${n2} = ${result}`;
        }

    }

    console.log(outputMessage)
}

// operationsBetweenNumbers(12, 10, "-")
// operationsBetweenNumbers(10, 12, "+")