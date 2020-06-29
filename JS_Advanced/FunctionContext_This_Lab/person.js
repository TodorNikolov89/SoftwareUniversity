function Person(first, last) {
    this.firstName = first;
    this.lastName = last;

    Object.defineProperty(this, "fullName", {
        set: function (value) {
            let names = value.split(" ");
            if (names.length !== 2) {
                return;
            }
            this.firstName = names[0];
            this.lastName = names[1];
        },
        get: function (value) {
            value = this.firstName + " " + this.lastName
            return value
        }
    })
}

// let person = new Person("Peter", "Ivanov");
// console.log(person.fullName);//Peter Ivanov
// person.firstName = "George";
// console.log(person.fullName);//George Ivanov
// person.lastName = "Peterson";
// console.log(person.fullName);//George Peterson
// person.fullName = "Nikola Tesla";
// console.log(person.firstName)//Nikola
// console.log(person.lastName)//Tesla


let person = new Person("Albert", "Simpson");
console.log(person.fullName);//Albert Simpson
person.firstName = "Simon";
console.log(person.fullName);//Simon Simpson
person.fullName = "Peter";
console.log(person.firstName) // Simon
console.log(person.lastName) // Simpson

