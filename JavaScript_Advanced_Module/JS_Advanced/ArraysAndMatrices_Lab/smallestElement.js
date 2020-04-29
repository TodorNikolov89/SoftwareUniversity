function smallestElement(input) {
    let arr = input.sort(compareNumbers).slice(0,2);
    console.log(arr.join(" "))   

    function compareNumbers(a, b) {
        return a - b;
    }
}

biggestelement([3, 0, 10, 4, 7, 3])