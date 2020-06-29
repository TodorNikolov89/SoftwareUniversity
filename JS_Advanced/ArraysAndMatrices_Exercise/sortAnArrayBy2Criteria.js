function sortAnArrayBy2Criteria(input) {

    input.sort(twoCriteriaSort)
    input.forEach(el => console.log(el))

    function twoCriteriaSort(cur, next) {
        if (cur.length > next.length) {
            return 1
        }
        if (cur.length < next.length) {
            return -1
        }
        let res = cur.localeCompare(next)
        return res
    }
}

sortAnArrayBy2Criteria(['test',
    'Deny',
    'omen',
    'Default']
)