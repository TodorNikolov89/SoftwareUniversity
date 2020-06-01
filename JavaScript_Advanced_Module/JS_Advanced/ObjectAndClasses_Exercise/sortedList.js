class List {
    constructor() {
        this.list = [];
        this.size = 0;
    }

    add(elemenent) {
        this.list.push(elemenent)
        this.list.sort((a, b) => a - b);
        this.size++;
    }

    remove(index) {
        this.checkIndex(index)
        this.list.splice(index, 1)
        this.size--;
    }

    get(index) {
        this.checkIndex(index)
        return this.list[index];
    }

    checkIndex(index) {
        if (index < 0 || index >= this.list.length) {
            throw new Error('Index is out of bound!')
        }
    }
}

let list = new List();
list.add(5);
list.add(6);
list.add(7);
console.log(list.get(1));
list.remove(1);
console.log(list.get(1));

