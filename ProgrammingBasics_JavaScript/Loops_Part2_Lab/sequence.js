function sequence(n){
    let lastNumber = 0;
    let nextNumber = 2 * lastNumber + 1;
    while(nextNumber <= n){
        console.log(nextNumber)
        lastNumber = nextNumber;
        nextNumber = 2 * lastNumber + 1;

    }
}

// sequence(17)