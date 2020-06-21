function pressHouse() {

    class Article {
        constructor(title, content) {
            this.title = title;
            this.content = content;
        }

        toString() {
            return `Title: ${this.title}\nContent: ${this.content}`
        }
    }

    class ShortReports extends Article {
        constructor(title, content, originalResearch) {
            super(title, content);
            this.originalResearch = Object.assign(originalResearch)
            this.comments = [];

        }

        get content() {
            return this._content;
        }
        set content(content) {
            if (content.length >= 150) {
                throw new Error(`Short reports content should be less then 150 symbols.`)
            }

            this._content = content;
        }

        set originalResearch(originalResearch) {
            if (!originalResearch.hasOwnProperty('author') || !originalResearch.hasOwnProperty('title')) {
                throw new Error(`The original research should have author and title.`)
            }

            this._originalResearch = Object.assign(originalResearch)
        }

        get originalResearch() {
            return this._originalResearch;
        }

        addComment(comment) {
            this.comments.push(comment);
            return `The comment is added.`
        }

        toString() {
            let result = super.toString();
            result += `\nOriginal Research: ${this.originalResearch.title} by ${this.originalResearch.author}`
            let allComments = '';
            if (this.comments.length > 0) {
                allComments += '\nComments:'
                this.comments.forEach(comment => {
                    allComments += `\n${comment}`
                });
            }
            return result + allComments;
        }
    }

    class BookReview extends Article {
        constructor(title, content, book) {
            super(title, content);
            this.book = Object.assign(book)
            this.clients = []
        }

        addClient(clientName, orderDescription) {
            let client = {}
            if (this.clients.some(c => c.clientName === clientName)) {
                client = this.clients.filter(c => {
                    if (c.orderDescription === orderDescription) {
                        throw new Error(`This client has already ordered this review.`)
                    }
                })

            }

            this.clients.push({ clientName: clientName, orderDescription: orderDescription })
            return `${clientName} has ordered a review for ${this.book.name}`
        }

        toString() {
            let result = super.toString() + '\n';
            result += `Book: ${this.book.name}`
            if (this.clients.length > 0) {
                result += `\nOrders:`
                this.clients.forEach(c => {
                    result += `\n${c.clientName} - ${c.orderDescription}`
                });
            }

            return result;
        }
    }

    return {
        Article,
        ShortReports,
        BookReview
    }
}

let classes = new pressHouse();
let book = new classes.BookReview("The Great Gatsby is so much more than a love story", "The Great Gatsby is in many ways similar to Romeo and Juliet, yet I believe that it is so much more than just a love story. It is also a reflection on the hollowness of a life of leisure. ...", { name: "The Great Gatsby", author: "F Scott Fitzgerald" });
console.log(book.addClient("The Guardian", "100 symbols"));
console.log(book.addClient("The Guardian", "100 sasymbols"));
console.log(book.toString()); 
