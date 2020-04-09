function bonusScore(input){
    let score  = 0;
    let number = Number(input.shift())
    if(number<=100){
        score +=5;
    } else if (number>100 && number<=1000){
        score += number * 0.20;
    } else if (number > 1000){
        score += number * 0.10;
    }
     
    if(number % 2 == 0){
        score +=1;
    }
    
    let lastdigit = number % 10;

    if(lastdigit === 5){
        score +=2;
    }

    console.log(score);
    console.log(number+score)
}

// bonusScore(20)
// bonusScore(175)
// bonusScore(2703)
// bonusScore(15875)