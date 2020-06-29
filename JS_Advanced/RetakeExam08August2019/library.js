class Library {
    constructor(libraryName) {
        this.libraryName = libraryName.toString();
        this.subscribers = [];
        this.subscriptionTypes = {
            normal: (libraryName) => libraryName.length,
            special: (libraryName) => libraryName.length * 2,
            vip: () => Number.MAX_SAFE_INTEGER
        }
    }

    subscribe(name, type) {
        if (type !== 'normal' && type !== 'special' && type !== 'vip') {
            throw new Error(`The type ${type} is invalid`)
        }

        let person = this.subscribers.find(p => p.name === name)
        if (person === undefined) {
            person = {
                name: name,
                type: type,
                books: []
            }

            this.subscribers.push(person)
        } else {
            person.type = type;
        }

        return person;
    }

    unsubscribe(name) {
        let person = this.subscribers.find(p => p.name === name);
        if (person === undefined) {
            throw new Error(`There is no such subscriber as ${name}`);
        }

        const index = this.subscribers.indexOf(person);
        if (index > -1) {
            this.subscribers.splice(index, 1);
        }

        return this.subscribers;
    }

    receiveBook(subscriberName, bookTitle, bookAuthor) {
        let person = this.subscribers.find(p => p.name === subscriberName);
        if (person === undefined) {
            throw new Error(`There is no such subscriber as ${subscriberName}`);
        }
        let personBooks = person.books.length;
        let limit = this.subscriptionTypes[person.type](this.libraryName);
        if (limit > personBooks) {
            person.books.push({
                title: bookTitle,
                author: bookAuthor
            })
        } else {
            throw new Error(`You have reached your subscription limit ${limit}!`)
        }
        return person;
    }

    showInfo() {
        if (this.subscribers.length === 0) {
            return `${this.libraryName} has no information about any subscribers`
        }

        let result = '';
        for (const subs of this.subscribers) {
            result += `Subscriber: ${subs.name}, Type: ${subs.type}\n`
            result += `Received books: `
            let receivedBooks = [];
            for (const book of subs.books) {
                receivedBooks.push(`${book.title} by ${book.author}`)
            }
            result += receivedBooks.join(", ") + '\n'
        }
        return result;
    }
}


let lib = new Library('Lib');

lib.subscribe('Peter', 'normal');
lib.subscribe('John', 'special');

lib.receiveBook('John', 'A Song of Ice and Fire', 'George R. R. Martin');
lib.receiveBook('Peter', 'Lord of the rings', 'J. R. R. Tolkien');
lib.receiveBook('John', 'Harry Potter', 'J. K. Rowling');

console.log(lib.showInfo());
