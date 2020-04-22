function suitcasesLoad(input) {
    let trunkVolume = Number(input.shift());
    let newVolume = input.shift();
    let counter = 0;
    let AdditionalVolume = 10;
    let outputMessage = ``;
    let isFilled = false;

    while (newVolume !== 'End') {
        counter++;
        newVolume = Number(newVolume);

        if (counter % 3 === 0) {
            newVolume += Number((newVolume * AdditionalVolume / 100).toFixed(2));
        }      
        
        if (trunkVolume - newVolume < 0) {
            isFilled = true;
            outputMessage = `No more space!`;
            counter--;
            break;
        }

        trunkVolume -= newVolume;

        newVolume = input.shift();
    }

    if (!isFilled) {
        outputMessage = `Congratulations! All suitcases are loaded!`;
    }

    console.log(outputMessage);
    console.log(`Statistic: ${counter} suitcases loaded.`)

}

suitcasesLoad([550, 100, 252, 72, "End"])
// suitcasesLoad([700.5, 180, 340.6, 126, 220, "End"])
// suitcasesLoad([1200.2, 260, 380.5, 125.6, 305.5, 'End'])
// suitcasesLoad([120, 'End'])