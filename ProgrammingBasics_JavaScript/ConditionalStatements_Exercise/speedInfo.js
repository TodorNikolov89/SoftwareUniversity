function speedInfo(input){
    let speed = Number(input);
    let outputMessage = "";

    if(speed <= 10){
        outputMessage = "slow";
    } else if(speed>10 && speed<=50){
        outputMessage = "average";
    } else if(speed>50 && speed<=150){
        outputMessage = "fast"
    } else if(speed > 150 && speed<=1000){
        outputMessage = "ultra fast";
    } else {
        outputMessage = "extremely fast"
    }

    console.log(outputMessage);
}

// speedInfo(8)
// speedInfo(49.5)
// speedInfo(126)
// speedInfo(160)
// speedInfo(3500)