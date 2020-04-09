function solve(inputSeconds, inputMetters, inputSecPerMetter){
    let recordSeconds = Number(inputSeconds);
    let distance = Number(inputMetters);
    let timePerMetter = Number(inputSecPerMetter);

    let time =  distance * timePerMetter;
    let lag = Math.floor(distance / 15);
    let finalTime = time+lag*12.5;  

    let outputMesssage = "";
    if(time > recordSeconds){
        let difference = finalTime - recordSeconds;
        outputMesssage = `No, he failed! He was ${difference.toFixed(2)} seconds slower.`;
    } else {        
        outputMesssage = `Yes, he succeeded! The new world record is ${finalTime.toFixed(2)} seconds.`;
    }
        
    console.log(outputMesssage);
}

solve(10464, 1500, 20)
solve(55555.67, 3017, 5.03)
