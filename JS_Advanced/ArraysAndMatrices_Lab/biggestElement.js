function biggestElement(input) {
    let biggest = Number.MIN_SAFE_INTEGER;
    for (let el of input) {
        let biggestNum = el.sort(compareNumbers).reverse().shift()
        if (biggestNum >= biggest) {
            biggest = biggestNum;
        }
    }

    console.log(biggest)

    function compareNumbers(a, b) {
        return a - b;
    }
}

biggestElement([[3, 5, 7, 12],
    [-1, 4, 33, 2],
    [8, 3, 0, 4]]
   )