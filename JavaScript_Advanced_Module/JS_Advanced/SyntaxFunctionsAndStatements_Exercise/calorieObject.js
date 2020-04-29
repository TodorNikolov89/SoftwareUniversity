function calorieObject(input) {
    let obj;
    let calories;
    let result = '{ '

    for (let i = 0; i < input.length; i += 2) {
        obj = input[i]
        calories = input[i + 1];
        let newStr = obj + ": " + calories + ", ";
        result += newStr;
    }
    result = result.substring(0, result.length - 2) + " }";

    console.log(result)
}

// calorieObject(['Yoghurt', '48', 'Rise', '138', 'Apple', '52'])