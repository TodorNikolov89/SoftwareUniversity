function squareOfStars(arg) {
    let inputType = typeof arg;
    let size;
    if (inputType==="undefined") {
        size = 5;
    } else {
        size = Number(arg);
    }

    let row='';

    for (let i = 0; i < size; i++) {
        for (let j = 0; j < size; j++) {
            row += '*'+" "
        }
        row+='\n'
    }

    console.log(row)
}

squareOfStars()