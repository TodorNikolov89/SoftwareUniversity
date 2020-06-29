function storeCatalogue(input) {
    let catalogue = new Map();
    let groups = new Set();

    for (const element of input) {
        let line = element.split(" : ");
        let price = Number(line[1]);
        let name = line[0];
        let ch = name.charAt(0);
        catalogue.set(name, price);
        groups.add(ch);
    }

    for (const group of Array.from(groups).sort()) {
        console.log(group)
        for (const prod of Array.from(catalogue.keys()).sort()) {
            if(prod.startsWith(group)){
                console.log(`  ${prod}: ${catalogue.get(prod)}`)
            }
           
        }
    }
}


storeCatalogue([
    'Appricot : 20.4',
    'Fridge : 1500',
    'TV : 1499',
    'Deodorant : 10',
    'Boiler : 300',
    'Apple : 1.25',
    'Anti-Bug Spray : 15',
    'T-Shirt : 10'
])