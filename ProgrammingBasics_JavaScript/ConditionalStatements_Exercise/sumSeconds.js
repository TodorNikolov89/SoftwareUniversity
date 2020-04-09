function sumSeconds(time1, time2, time3){
    let allSeconds = Number(time1) + Number(time2) +Number(time3);
    let minutes = Math.floor( allSeconds / 60);
    let seconds = allSeconds % 60;

    if(seconds>9){
        console.log(`${minutes}:${seconds}`)
    } else {
        console.log(`${minutes}:0${seconds}`)
    }
}

// sumSeconds(35,45,44)
// sumSeconds(22, 7, 34)
// sumSeconds(50, 50, 49)
// sumSeconds(14,12,10)