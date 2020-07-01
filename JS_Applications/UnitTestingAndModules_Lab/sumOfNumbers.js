function sum(arr) {
    let result = 0;

    for (const number of arr) {
        result += Number(number);
    }

    return result;
}

module.exports = sum;