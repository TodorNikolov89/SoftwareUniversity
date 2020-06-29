function magicMatrices(input) {
    let sum = input[0].reduce((a, b) => a + b);
    let isMagic = true;

    var rowSum = input.map(r => r.reduce((a, b) => a + b));
    var colSum = input.reduce((a, b) => a.map((x, i) => x + b[i]));
    let equalRows = rowSum.every(v => v === rowSum[0])
    let equalCols = colSum.every(v => v === colSum[0])

    console.log(equalRows && equalCols);
}

magicMatrices([[4, 5, 6],
[6, 5, 4],
[5, 5, 5]]
)


// magicMatrices([[11, 32, 45],
// [21, 0, 1],
// [21, 1, 1]]
// )