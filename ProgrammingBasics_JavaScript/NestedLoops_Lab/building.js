function building(input) {
    floors = Number(input.shift());
    rooms = Number(input.shift());
    let printRow = '';
    let isLastFloor = true;
    for (let floor = floors; floor >= 1; floor--) {
        for (let room = 0; room < rooms; room++) {
            if (floor === floors) {
                printRow += `L${floor}${room} `
            } else if (floor % 2 === 0) {
                printRow += `O${floor}${room} `
            } else {
                printRow += `A${floor}${room} `
            }

            isLastFloor = false;
        }
        printRow = printRow.substring(0, printRow.length - 1);
        console.log(printRow);
        printRow = '';
    }
}

// building([6, 4])