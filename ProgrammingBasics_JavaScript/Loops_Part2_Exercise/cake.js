function cake(input) {
    let width = Number(input.shift());
    let heigth = Number(input.shift());    
    let totalPieces = width * heigth;

    let pieces = input.shift();
    let outputMessage = '';

    while (totalPieces >= 0 && pieces !== 'STOP') {
        pieces = Number(pieces);

        totalPieces -= pieces;
        pieces = input.shift();
    }

    if (totalPieces >= 0) {
        outputMessage = `${totalPieces} pieces are left.`;
    } else {
        outputMessage = `No more cake left! You need ${Math.abs(totalPieces)} pieces more.`;
    }

    console.log(outputMessage);
}

// cake([10, 10, 20, 20, 20, 20, 21])
// cake([10, 2, 2, 4, 6,'STOP'])