function sameNumbers(number) {
    number = Number(number);
    let sum = 0;
    let lastNum = number % 10;
    let areSame = true;
    while (number) {
        let temp = number % 10;
        if (lastNum !== temp) {
            areSame = false;
        }
        lastNum = temp;
        sum += temp;
        number = Math.floor(number / 10);
    }
    console.log(`${areSame}\n${sum}`);
}

sameNumbers(2222222)