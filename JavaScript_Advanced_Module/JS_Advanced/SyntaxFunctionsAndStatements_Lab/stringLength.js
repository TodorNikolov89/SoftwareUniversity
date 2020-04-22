function stringLength(arr1, arr2, arr3) {
    let sumLength;
    let averageLength;

    let firstArg = arr1.length;
    let secondArg = arr2.length;
    let thirdArg = arr3.length;

     sumLength = firstArg + secondArg + thirdArg;
     averageLength = Math.floor(sumLength / 3);

    console.log(sumLength)
    console.log(averageLength)
}

stringLength('chocolate', 'ice cream', 'cake')