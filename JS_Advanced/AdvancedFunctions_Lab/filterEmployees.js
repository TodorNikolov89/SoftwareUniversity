function filterEmployees(input, crit) {
    let criteria = crit.split("-");
    let text = JSON.parse(input);
    let sorted = [];

    if (criteria.length === 1) {
        let i = 0;
        printeResult(text)
    } else {
        let key = criteria[0];
        let value = criteria[1];
        for (let obj of text) {
            if (hasValue(obj, key, value)) {
                sorted.push(obj)
            }
        }
        printeResult(sorted)
    }

    function hasValue(obj, key, value) {
        return obj.hasOwnProperty(key) && obj[key] === value;
    }

    function printeResult(obj) {
        let i = 0;
        for (let line of obj) {
            console.log(`${i}. ${line["first_name"]} ${line["last_name"]} - ${line["email"]}`);
            i++;
        }
    }
}

filterEmployees(
    `[{
    "id": "1",
    "first_name": "Ardine",
    "last_name": "Bassam",
    "email": "abassam0@cnn.com",
    "gender": "Female"
  }, {
    "id": "2",
    "first_name": "Kizzee",
    "last_name": "Jost",
    "email": "kjost1@forbes.com",
    "gender": "Female"
  },  
{
    "id": "3",
    "first_name": "Evanne",
    "last_name": "Maldin",
    "email": "emaldin2@hostgator.com",
    "gender": "Male"
  }]`,
    'gender-Female'
)
