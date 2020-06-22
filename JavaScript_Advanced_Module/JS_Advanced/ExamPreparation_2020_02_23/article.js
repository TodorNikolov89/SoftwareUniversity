class Article {
    #likes;
    #comments;
    constructor(title, creator) {
        this.title = title;
        this.creator = creator;
        this.#comments = [];
        this.#likes = [];

    }

    get likes() {
        if (this.#likes.length === 0) {
            return `${this.title} has 0 likes`
        } else if (this.#likes.length === 1) {
            return `${this.#likes[0]} likes this article!`
        }

        let first = this.#likes[0];
        return `${first} and ${this.#likes.length - 1} others like this article!`
    }

    like(username) {
        let person = this.#likes.find(p => p === username);
        if (person !== undefined) {
            throw new Error(`You can't like the same article twice!`);
        }

        if (person === this.creator) {
            throw new Error(`You can't like your own articles!`);
        }

        this.#likes.push(username);
        return `${username} liked ${this.title}!`
    }

    dislike(username) {
        let person = this.#likes.find(p => p === username);
        if (person === undefined) {
            throw new Error(`You can't dislike this article!`);
        }

        const index = this.#likes.indexOf(person);
        if (index > -1) {
            this.#likes.splice(index, 1);
        }

        return `${username} disliked ${this.title}`
    }

    comment(username, content, id) {

        if (id === undefined || !this.#comments.some(c => c.Id === id)) {
            let lastCommentId;
            if (this.#comments.length > 0) {
                lastCommentId = this.#comments[this.#comments.length - 1].Id;
            } else {
                lastCommentId = 0
            }

            let newComment = {
                Id: ++lastCommentId,
                Username: username,
                Content: content,
                Replies: []
            }

            this.#comments.push(newComment)

            return `${username} commented on ${this.title}`
        } else {
            let comment = this.#comments.find(c => c.Id === id);
            let replyId;
            if (comment.Replies.length > 0) {
                replyId = comment.Replies[comment.Replies.length - 1].Id
            } else {
                replyId = comment.Id;
            }
            replyId = (replyId + 0.1).toFixed(1)
            let newReply = {
                Id: Number(replyId),
                Username: username,
                Content: content,
            }

            comment.Replies.push(newReply)

            return `You replied successfully`
        }
    }

    toString(sortingType) {
        let sorted = [];
        let result = '';
        if (sortingType === 'asc') {
            sorted = this.#comments.sort(this.ascCompare);
            sorted.forEach(element => {
                element.Replies.sort(this.ascCompare)
            });
        } else if (sortingType === 'desc') {
            sorted = this.#comments.sort((a, b) => b.Id - a.Id);
            sorted.forEach(element => {
                element.Replies.sort(this.descCompare)
            });
        } else if (sortingType === 'username') {
            sorted = this.#comments.sort(this.ascCompareByUsername);
            sorted.forEach(element => {
                element.Replies.sort(this.ascCompareByUsername)
            });
        }

        result = `Title: ${this.title}\n`
        result += `Creator: ${this.creator}\n`
        result += `Likes: ${this.#likes.length}\n`
        result += `Comments:`
        for (const c of sorted) {
            result += `\n-- ${c.Id}. ${c.Username}: ${c.Content}`
            if (c.Replies.length > 0) {
                for (const r of c.Replies) {
                    result += `\n--- ${r.Id}. ${r.Username}: ${r.Content}`
                }
            }
        }

        return result;
    }

    ascCompare(a, b) {
        if (a.Id < b.Id) {
            return -1;
        }
        if (a.Id > b.Id) {
            return 1;
        }
        return 0;
    }

    descCompare(a, b) {
        if (a.Id < b.Id) {
            return 1;
        }
        if (a.Id > b.Id) {
            return -1;
        }
        return 0;
    }

    ascCompareByUsername(a, b) {
        if (a.Username < b.Username) {
            return -1;
        }
        if (a.Username > b.Username) {
            return 1;
        }
        return 0;
    }
}

let art = new Article("My Article", "Anny");
// art.like("John");
// console.log(art.likes);
// art.dislike("John");
// console.log(art.likes);
// art.comment("Sammy", "Some Content");
// console.log(art.comment("Ammy", "New Content"));
// art.comment("Zane", "Reply", 1);
// art.comment("Jessy", "Nice :)");
// console.log(art.comment("SAmmy", "Reply@", 1));
// console.log()
// console.log(art.toString('username'));
// console.log()
// art.like("Zane");
// console.log(art.toString('desc'));

art.like("John")//, "John liked My Article!");
console.log(art.likes)//, "John likes this article!");
//console.log(art.dislike("Sally"))//, "You can't dislike this article!");
console.log(art.like("Ivan"))//,"Ivan liked My Article!");
console.log(art.like("Steven"))//, "Steven liked My Article!");
console.log(art.likes)//, "John and 2 others like this article!");
console.log(art.comment("Anny", "Some Content"))//,"Anny commented on My Article");
console.log(art.comment("Ammy", "New Content", 1))//,"You replied successfully");
console.log(art.comment("Zane", "Reply", 2))//,"Zane commented on My Article");
console.log(art.comment("Jessy", "Nice :)"))//, "Jessy commented on My Article");
console.log(art.comment("SAmmy", "Reply@", 2))//, "You replied successfully")
console.log(art.toString('asc'))

// console.log(art.like("John"))//,"John liked My Article!");
// console.log(art.likes)//,"John likes this article!");
// console.log(art.dislike("John"))//, "John disliked My Article");
// console.log(art.likes)//, "My Article has 0 likes");
// console.log(art.comment("Sammy", "Some Content"))//, "Sammy commented on My Article");
// console.log(art.comment("Ammy", "New Content"))//, "Ammy commented on My Article");
// console.log(art.comment("Zane", "Reply", 1))//, "You replied successfully");
// console.log(art.comment("Jessy", "Nice :)"))//, "Jessy commented on My Article");
// console.log(art.comment("SAmmy", "Reply@", 1))//, "You replied successfully");
// console.log()
// console.log(art.toString('username'));
// console.log(art.like("Zane"))//, "Zane liked My Article!");
// console.log(art.toString('desc'))