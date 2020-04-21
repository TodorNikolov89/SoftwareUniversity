function mountainRun(input) {
    let record = Number(input.shift());
    let distance = Number(input.shift());
    let timePerMetter = Number(input.shift());

    let time = distance * timePerMetter;
    let additionalSeconds = Math.floor(distance / 50) * 30;
    time += additionalSeconds;
    let outputMessage = ``;

    if (time < record) {
        outputMessage = `Yes! The new record is ${time.toFixed(2)} seconds.`;
    } else {
        outputMessage = `No! He was ${(time - record).toFixed(2)} seconds slower.`;
    }

    console.log(outputMessage);
}

// mountainRun([10164, 1400,25])
// mountainRun([1377,389,3])