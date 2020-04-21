function catWalking(input) {
    let timeForWalk = Number(input.shift());
    let numberOfWalks = Number(input.shift());
    let calloriesPeDay = Number(input.shift());

    let totalMinutes = timeForWalk * numberOfWalks;
    let totalCallories = totalMinutes * 5;
    let outputMessage = ``;

    if (totalCallories >= calloriesPeDay * 0.50) {
        outputMessage = `Yes, the walk for your cat is enough. Burned calories per day: ${totalCallories}.`;
    } else {
        outputMessage = `No, the walk for your cat is not enough. Burned calories per day: ${totalCallories}.`;
    }


    console.log(outputMessage);
}

catWalking([15, 2, 500])