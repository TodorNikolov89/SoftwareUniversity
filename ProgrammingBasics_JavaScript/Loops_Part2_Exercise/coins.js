function coins(change) {
    let totalCoins = 0;
    change = Number(Math.floor(change * 100));
    let coins = 0;
    let coin = 200;

    while (change > 0) {
        if (change >= coin) {
            coins++;
            change -= coin;
        } else {
            coin = Math.floor(coin / 2)
            coin = coin === 25 ? 20 : coin;
        }
    }

    console.log(coins)
}

coins(2.73)