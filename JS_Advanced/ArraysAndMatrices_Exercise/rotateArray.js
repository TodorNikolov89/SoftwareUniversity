function rotateArray(input) {
    let rotation = Number(input.pop());
    rotation = rotation % input.length;

    for (let i = 0; i < rotation; i++) {
        let element = input.pop();
        input.unshift(element);
    }

    let result = '';
    for (let i = 0; i < input.length; i++) {
        result += input[i] + " ";
    }

    console.log(result);
}

// rotateArray(['Banana',
//     'Orange',
//     'Coconut',
//     'Apple',
//     '15']
// )