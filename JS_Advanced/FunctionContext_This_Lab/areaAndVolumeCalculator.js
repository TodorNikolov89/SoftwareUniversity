function solve(area, vol, input) {
    let objects = JSON.parse(input);
    function calc(obj) {
        let areaObj = Math.abs(area.call(obj))
        let volObj = Math.abs(vol.call(obj))

        return { area: areaObj, volume: volObj }
    }
    return objects.map(calc)
}


function vol() {
    return this.x * this.y * this.z;
};

function area() {
    return this.x * this.y;
};


solve(
    area, vol, `[{"x":"1","y":"2","z":"10"},
{"x":"7","y":"7","z":"10"},
{"x":"5","y":"2","z":"10"}
]`
)