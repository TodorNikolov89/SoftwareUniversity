class Company {
    constructor() {
        this.departments = new Map();
    }

    addEmployee(username, salary, position, department) {
        if (username === "" || username === undefined || username === null
            || salary === "" || salary === undefined || salary === null
            || position === "" || position === undefined || position === null
            || department === "" || department === undefined || department === null) {
            throw new Error("Invalid input!")
        }

        if (salary < 0) {
            throw new Error(' Invalid input!')
        }

        if (!this.departments.get(department)) {
            let arr = [];
            this.departments.set(department,arr)
        }

        this.departments.get(department).push({ username, salary, position })

        return `New employee is hired. Name: ${username}. Position: ${position}`
    }
}

let c = new Company();
c.addEmployee("Stanimir", 2000, "engineer", "Construction");
c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
c.addEmployee("Slavi", 500, "dyer", "Construction");
c.addEmployee("Stan", 2000, "architect", "Construction");
c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
c.addEmployee("Gosho", 1350, "HR", "Human resources");
console.log()
