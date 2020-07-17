//Important:
//ENTER:
const appId = '0A3092E3-587E-B6F7-FF4C-C25EF7B6D700';
const apiKey = 'DEA9F2DB-5A27-485F-9EDD-B1A7210D59FF'

function host(endpoints) {
    return `https://api.backendless.com/${appId}/${apiKey}/data/${endpoints}`
}

export async function getBooks() {
    const response = await fetch(host('books'))
    const data = await response.json();
    return data;
}

export async function createBook(book) {
    const response = await fetch(host('books'), {
        method: 'POST',
        body: JSON.stringify(book),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const data = await response.json();
    return data;
}

export async function updateBook(book) {
    const bookId = book.objectId;
    const response = await fetch(host('books/' + bookId), {
        method: 'put',
        body: JSON.stringify(book),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const data = await response.json();
    return data;
}

export async function deleteBook(id) {
    const response = await fetch(host('books/' + id), {
        method: 'DELETE'
    });

    const data = await response.json();
    return data;
}