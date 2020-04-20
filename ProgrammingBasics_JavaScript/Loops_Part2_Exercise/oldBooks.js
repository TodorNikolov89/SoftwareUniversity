function oldBooks(input) {
    let bookName = input.shift();
    let books = Number(input.shift());
    let outputMessage = '';
    let currentBookName = input.shift();
    let isFound = false;
    for (let i = 1; i <= books; i++) {
        if (bookName === currentBookName) {
            outputMessage = `You checked ${i - 1} books and found it.`;
            isFound = true;
            break;
        } else {
            currentBookName = input.shift();
        }
    }

    if (isFound) {
        console.log(outputMessage)
    } else {
        console.log(`The book you search is not here!\nYou checked ${books} books.`)
    }
}

// oldBooks(["Troy", 8, "Stronger", "Life Style", "Troy"])
// oldBooks(["The Spot", 4, "Hunger Games", "Harry Potter", "Torronto", "Spotify",])