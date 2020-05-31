function townsToJSON(input) {
    let towns = [];
    let regex = /\s*\|\s*/;

    for (let line of input.splice(1)) {
        let tokens = line.split(regex);
        let latitude = Number(tokens[2]).toFixed(2);
        let longitude = Number(tokens[3]).toFixed(2);
        let townObj = { Town: tokens[1], Latitude: parseFloat(latitude), Longitude: parseFloat(longitude) };
        towns.push(townObj);
    }

    console.log(JSON.stringify(towns));
}


townsToJSON([
    '| Town | Latitude | Longitude |',
    '| Sofia | 42.696552 | 23.32601 |',
    '| Beijing | 39.913818 | 116.363625 |'
])