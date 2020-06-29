function mathOperations(arr1, arr2, arr3) {
    let num1 = Number(arr1);
    let num2 = Number(arr2)
    let operation = arr3;

    let result;


    switch (operation) {
        case '+':
            result = num1 + num2
            break;
        case '-':
            result = num1 - num2
            break;
        case '*':
            result = num1 * num2
            break;
        case '/':
            result = num1 / num2
            break;
        case '%':
            result = num1 % num2
            break;
        case '**':
            result = num1 ** num2
            break;
    }

    console.log(result)
}