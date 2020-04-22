function sumOfNumbers(num1, num2) {
    let number1 = Number(num1);
    let number2 = Number(num2);
    let result = 0;

    for (let i = number1; i <= number2; i++) {
        result += Number(i);
    }

    console.log(result)

}

sumOfNumbers('1','5')