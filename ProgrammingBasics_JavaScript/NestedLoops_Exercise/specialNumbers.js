function specialNumbers(n) {
    let temp = 0;
    let result = '';
    let isSpecial = true;

    for (let i = 1111; i < 10000; i++) {
        isSpecial = true;
        let num = i;
        while (num > 0) {
            temp = num % 10;
            num = Math.floor(num / 10);

            if (n % temp !== 0) {
                isSpecial = false;
            }
        }

        if (isSpecial) {
            let str = i.toString();
            result += str + " ";
        }
    }

    console.log(result)
}

// specialNumbers(16);