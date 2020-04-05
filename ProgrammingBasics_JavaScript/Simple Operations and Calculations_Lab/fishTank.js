function calculateAquariumVolume(length, width, heigth, percent){
    let aquariumVolume = length * width * heigth;
    let waterVolume = (aquariumVolume - aquariumVolume*(percent/100))*0.001;

    console.log(waterVolume.toFixed(3))
}

// calculateAquariumVolume(85, 75, 47, 17)
// calculateAquariumVolume(105, 77, 89, 18.5)