function smallShop(bevarage, town, amount){
    amount = Number(amount);
    let price = 0;
    let total = 0;
    switch(bevarage){
        case 'coffee':
            if(town==='Sofia'){
                price = 0.50;
            } else if( town ==='Plovdiv'){
                price = 0.40
            } else if(town === 'Varna'){
                price = 0.45;
            }
        break;
        case 'water':
            if(town==='Sofia'){
                price = 0.80;
            } else if( town ==='Plovdiv'){
                price = 0.70
            } else if(town === 'Varna'){
                price = 0.70;
            }
        break;
        case 'beer':
            if(town==='Sofia'){
                price = 1.20;
            } else if( town ==='Plovdiv'){
                price = 1.15;
            } else if(town === 'Varna'){
                price =1.10;
            }
        break;
        case 'sweets':
            if(town==='Sofia'){
                price = 1.45;
            } else if( town ==='Plovdiv'){
                price = 1.30;
            } else if(town === 'Varna'){
                price = 1.35;
            }
        break;
        case 'peanuts':
            if(town==='Sofia'){
                price = 1.60;
            } else if( town ==='Plovdiv'){
                price = 1.50;
            } else if(town === 'Varna'){
                price = 1.55;
            }
        break;
    }

    total = price * amount;

    console.log(total)
}

// smallShop('coffee', 'Varna', 2)