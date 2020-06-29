function timeToWalk(steps, footPrintInMetters, speed) {
    steps = Number(steps);
    footPrintInMetters = Number(footPrintInMetters);
    speed = Number(speed);

    let distance = steps * footPrintInMetters / 1000;
    let addMinutes = Math.floor(distance / 0.5);

    let time = distance / speed;
    let minutes = time * 60 + addMinutes;
    let seconds = Math.ceil(minutes * 60);

    var date = new Date(null);
    date.setSeconds(seconds); // specify value for SECONDS here
    var result = date.toISOString().substr(11, 8);

    console.log(result);
}

// timeToWalk(2564, 0.70, 5.5)