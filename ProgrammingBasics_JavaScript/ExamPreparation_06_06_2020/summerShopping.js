function solve(input) {
    let budged = Number(input.shift());
    let towelPrice = Number(input.shift());
    let discount = Number(input.shift());

    let umbrellaPrice = towelPrice * 2 / 3;
    let flipFlopsPrice = umbrellaPrice * 0.75;
    let bagPrice = (flipFlopsPrice + towelPrice) * 1 / 3;

    let message = ``;

    let neededMoney = towelPrice + umbrellaPrice + flipFlopsPrice + bagPrice;

    neededMoney -= neededMoney * discount / 100;
    let difference = Math.abs(budged - neededMoney);

    if (budged >= neededMoney) {
        message = `Annie's sum is ${neededMoney.toFixed(2)} lv. She has ${difference.toFixed(2)} lv. left.`;
    } else {
        message = `Annie's sum is ${neededMoney.toFixed(2)} lv. She needs ${difference.toFixed(2)} lv. more.`;
    }

    console.log(message);
}


solve(['30', '17', '3']);