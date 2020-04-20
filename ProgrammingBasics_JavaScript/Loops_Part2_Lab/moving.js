function moving(input) {
    let width = Number(input.shift());
    let length = Number(input.shift())
    let height = Number(input.shift());
    let roomVolume = width * length * height;
    let boxes = input.shift();

    while (boxes !== 'Done' && roomVolume >= 0) {
        boxes = Number(boxes);
        roomVolume -= boxes;

        boxes = input.shift();
    }
    let outputMessage = '';
    if (roomVolume < 0) {
        outputMessage = `No more free space! You need ${Math.abs(roomVolume)} Cubic meters more.`;
    } else {
        outputMessage = `${roomVolume} Cubic meters left.`;
    }

    console.log(outputMessage);
}

moving([10, 10, 2, 20, 20, 20, 20, 122])
moving([10, 1, 2, 4, 6, "Done"])