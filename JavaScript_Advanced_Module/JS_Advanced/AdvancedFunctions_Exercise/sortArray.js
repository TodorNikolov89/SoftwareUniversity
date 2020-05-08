function sortArray(arr, sort) {
    let result = [];

    if (sort === 'asc') {
        result = sortAscending(arr)
        console.log(Array.from(result))
    } else if (sort === "desc") {
        result = sortAscending(Array.from(arr))
        console.log(result)
    }

    function sortAscending(arr) {
        return arr.sort((a, b) => a - b)
    }

    function sortDescending(arr) {
        return arr.sort((a, b) => b - a)
    }

}

sortArray([14, 7, 17, 6, 8], 'asc')