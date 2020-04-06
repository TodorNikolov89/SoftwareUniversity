function danceHall(lenght, width, wardrobeSide){
    let hallArea = 100 * lenght * 100 * width;
    let wadrobeArea = Math.pow(wardrobeSide*100,2);
    let benchArea = hallArea / 10;

    hallArea = hallArea - (wadrobeArea + benchArea);

    let numberOfDancers = Math.floor(hallArea/(7040));
    console.log(numberOfDancers)
}

// danceHall(50, 25, 2)
