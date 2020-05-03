function extractIncreasingSubsequenceFromAnArray(input) {
    let result = [];
    let lastElement = input.shift();
    result.push(lastElement);

    for (const element of input) {
        if (lastElement <= element) {
            result.push(element)
            lastElement = element;
        }
    }

    result.forEach(element => {
        console.log(element)
    });
}

// extractIncreasingSubsequenceFromAnArray(
//     [
//         1,
//         3,
//         8,
//         4,
//         10,
//         12,
//         3,
//         2,
//         24
//     ])