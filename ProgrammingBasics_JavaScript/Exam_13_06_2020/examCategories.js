function solve(diff, comp, pages) {
    diff = Number(diff)
    comp = Number(comp)
    pages = Number(pages)

    if (diff <= 10) {
        console.log('Elementary')
    } else if (diff <= 30 && pages <= 1) {
        console.log('Easy')
    } else if (diff >= 80 && comp >= 80 && pages >= 8) {
        console.log('Legacy')
    } else if (diff >= 80 && comp <= 10) {
        console.log('Master')
    } else {
        if (comp >= 50 && pages >= 2) {
            console.log('Hard')
        } else {
            console.log('Regular')
        }
    }
}

solve(90, 60, 10)
solve(80, 40, 3)
solve(40, 40, 0)