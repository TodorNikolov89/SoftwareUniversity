class Forum {
    #users;
    #questions;
    #id;
    constructor() {
        this.#users = [];
        this.#questions = [];
        this.#id = 1;
    }

    register(username, password, repeatPassword, email) {
        if (username === "" || password === "" || repeatPassword === "" || email === "") {
            throw new Error(`Input can not be empty`)
        }

        if (password !== repeatPassword) {
            throw new Error(`Passwords do not match`)
        }

        let user = this.#users.find(u => u.username === username || u.email === email)
        if (user !== undefined) {
            throw new Error('This user already exists!');
        }

        this.#users.push({
            username: username,
            password: password,
            email: email,
            isLoggedIn: false
        })

        return `${username} with ${email} was registered successfully!`
    }

    login(username, password) {
        let user = this.#users.find(u => u.username === username)
        if (user === undefined) {
            throw new Error('There is no such user');
        }

        if (user.password === password) {
            user.isLoggedIn = true;
            return `Hello! You have logged in successfully`
        }
    }

    logout(username, password) {
        let user = this.#users.find(u => u.username === username)
        if (user === undefined) {
            throw new Error('There is no such user');
        }

        if (user.password === password) {
            user.isLoggedIn = false;
            return `You have logged out successfully`
        }
    }

    postQuestion(username, question) {
        let user = this.#users.find(u => u.username === username)
        if (user === undefined || !user.isLoggedIn) {
            throw new Error('You should be logged in to post questions')
        }

        if (question === "") {
            throw new Error('Invalid question')
        }

        this.#questions.push({
            id: this.#id,
            content: question,
            creator: user,
            answers: []
        })
        this.#id++;

        return `Your question has been posted successfully`
    }

    postAnswer(username, questionId, answer) {
        let user = this.#users.find(u => u.username === username)
        if (user === undefined || !user.isLoggedIn) {
            throw new Error('You should be logged in to post answers')
        }

        if (answer === "") {
            throw new Error('Invalid answer')
        }

        let question = this.#questions.find(q => q.id === questionId)
        if (question === undefined) {
            throw new Error(`There is no such question`)
        }
        question.answers.push({
            username: username,
            content: answer
        });

        return `Your answer has been posted successfully`;
    }


    showQuestions() {
        let result = '';

        for (const q of this.#questions) {
            result += `Question ${q.id} by ${q.creator.username}: ${q.content}\n`
            for (const a of q.answers) {
                result += `---${a.username}: ${a.content}\n`
            }
        }
        result = result.trim();
        return result;
    }
}

let forum = new Forum();

forum.register('Michael', '123', '123', 'michael@abv.bg');
forum.register('Stoyan', '123ab7', '123ab7', 'some@gmail@.com');
forum.login('Michael', '123');
forum.login('Stoyan', '123ab7');

forum.postQuestion('Michael', "Can I rent a snowboard from your shop?");
forum.postAnswer('Stoyan', 1, "Yes, I have rented one last year.");
forum.postQuestion('Stoyan', "How long are supposed to be the ski for my daughter?");
forum.postAnswer('Michael', 2, "How old is she?");
forum.postAnswer('Michael', 2, "Tell us how tall she is.");

console.log(forum.showQuestions());


// let forum = new Forum();

// forum.register('Jonny', '12345', '12345', 'jonny@abv.bg');
// forum.register('Peter', '123ab7', '123ab7', 'peter@gmail@.com');
// forum.login('Jonny', '12345');
// forum.login('Peter', '123ab7');

// forum.postQuestion('Jonny', "Do I need glasses for skiing?");
// forum.postAnswer('Peter',1, "Yes, I have rented one last year.");
// forum.postAnswer('Jonny',1, "What was your budget");
// forum.postAnswer('Peter',1, "$50");
// forum.postAnswer('Jonny',1, "Thank you :)");

// console.log(forum.showQuestions());
