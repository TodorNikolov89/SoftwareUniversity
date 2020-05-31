function heroicInventory(input) {
    let obj = {};
    let resultArr = []
    for (const line of input) {

        let arr = line.split(/\W+/);
        let heroName = arr.shift();
        let heroLevel = Number(arr.shift());
        let items = [];

        for (const item of arr) {
            items.push(item);
        }

        let obj = { name: heroName, level: heroLevel, items: items };
        resultArr.push(obj);
    }

    console.log(JSON.stringify(resultArr));

}

heroicInventory([
    'Isacc / 25 / Apple, GravityGun',
    'Derek / 12 / BarrelVest, DestructionSword',
    'Hes / 1 / Desolator, Sentinel, Antara'
])