function careOfPuppy(input) {
    let boughtFood = Number(input.shift()) * 1000;
    let line = input.shift();
    let eatenFood = 0;

    while (line !== "Adopted") {
        line = Number(line);
        eatenFood += line;
        line = input.shift();
    }

    let outputMessage = '';
    if (eatenFood <= boughtFood) {
        outputMessage = `Food is enough! Leftovers: ${boughtFood - eatenFood} grams.`;
    } else {
        outputMessage = `Food is not enough. You need ${eatenFood - boughtFood} grams more.`;
    }

    console.log(outputMessage);
}

careOfPuppy([4, 130, 345, 400, 180, 230, 120, 'Adopted']);