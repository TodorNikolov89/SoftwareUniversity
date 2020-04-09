function calculateTime(inputHours, inputMinutes){
    let hours = Number(inputHours);
    let minutes = Number(inputMinutes);

    if(minutes + 15 > 59){
        minutes = minutes + 15 - 60;
        
        if(hours + 1 == 24){
            hours = 0;
        }
        else{
            hours++;
        }
    } else {
        minutes += 15;
        
    }

    if(minutes>9){
        console.log(hours + ":" + minutes)
    } else {
    console.log(`${hours}:0${minutes}`)
    }
}

// calculateTime(1, 46)
// calculateTime(0, 01)
// calculateTime(23,59)