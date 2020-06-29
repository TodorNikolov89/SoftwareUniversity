function cookingByNumbers(input) {
    let number = Number(input.shift());

    for (let action of input) {

        switch (action) {
            case 'chop':
                number /= 2;
                break;
            case 'dice':
                number = Math.sqrt(number);
                break;
            case 'spice':
                number += 1;
                break;
            case 'bake':
                number *= 3;
                break;
            case 'fillet':
                number -= number * 0.20;
                break;
        }

        console.log(number);
    }
}

cookingByNumbers(['32', 'chop', 'chop', 'chop', 'chop', 'chop'])
