function diagonalSums(input) {
    let mainDiagonal = 0;
    let secondDiagonal = 0;

    for (let i = 0; i < input.length; i++) {
        let arr = input[i];

        mainDiagonal += arr[i];
        secondDiagonal += arr[arr.length - 1 - i];
    }


    console.log(mainDiagonal, secondDiagonal)
}
