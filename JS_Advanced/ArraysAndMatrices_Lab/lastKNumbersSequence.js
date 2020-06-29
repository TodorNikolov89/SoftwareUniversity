function lastKNumbersSequence(n, k) {
    let arr = [1];
    let nextPar = 0;
    let start = 0;

    for (let i = 1; i < n; i++) {
        nextPar = 0;
        if (arr.length > k) {
            start = arr.length - k;
        }
        for (let j = start; j < arr.length; j++) {
            nextPar += arr[j];
        }

        arr.push(nextPar);
    }

    console.log(arr.join(" "))
}


lastKNumbersSequence(8, 2)