function solve(bees, health, attack) {
    bees = Number(bees)
    health = Number(health);
    attack = Number(attack);


    while (bees > 0 && health > 0) {
        bees -= attack;
        if (bees < 100) {
            if (bees < 0) {
                bees = 0;
            }
            break;
        }
        health -= bees * 5

        if (health <= 0) {
            break;
        }
    }

    if (health < 0) {
        console.log(`Beehive won! Bees left ${bees}.`)
    } else {
        console.log(`The bear stole the honey! Bees left ${bees}.`)
    }

}

// solve(200, 1000, 10)
// solve(200, 10000, 90)
// solve(200, 10000, 300)