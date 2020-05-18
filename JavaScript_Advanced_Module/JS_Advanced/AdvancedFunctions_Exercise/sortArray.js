function sortArray(a, b) {
    let result = filter();

    return result[b](a);
    function filter() {
        return {
            asc: (s) => s.sort((a, b) => a - b),
            desc: (s) => s.sort((a, b) => b - a)
        }
    }
}