function solve(bees, flowers) {
     bees = Number(bees);
     flowers = Number(flowers);

    let totalHoney = bees * flowers * 0.21;
    let combs =Math.floor(totalHoney / 100);
    let honeyLeft = totalHoney % 100;

    console.log(`${combs} honeycombs filled.`)
    console.log(`${honeyLeft.toFixed(2)} grams of honey left.`)
}

solve(['11','120'])