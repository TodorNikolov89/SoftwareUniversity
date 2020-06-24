function solve() {
    let inputForm = document.getElementsByTagName('form')[0];
    let addBtn = inputForm.lastElementChild;
    let bookShelf = document.getElementById('outputs');
    let oldBooks = bookShelf.children[0].lastElementChild
    let newBooks = bookShelf.children[1].lastElementChild
    let totalStoreProfit = document.getElementsByTagName('h1')[1];
    let totalProfit = 0;

    addBtn.addEventListener('click', (e) => {
        e.preventDefault();
        let title = inputForm.getElementsByTagName('input')[0].value;
        let year = inputForm.getElementsByTagName('input')[1].value;
        let price = inputForm.getElementsByTagName('input')[2].value;
        year = Number(year);
        price = year >= 2000 ? Number(price) : Number(price) - Number(price) * 0.15
        if (title !== "" && year > 0 && price > 0) {
            let div = document.createElement('div');
            div.className = 'book';

            let p = document.createElement('p');
            p.textContent = `${title} [${year}]`;

            let buyBtn = document.createElement('button');
            buyBtn.textContent = `Buy it only for ${price.toFixed(2)} BGN`
            buyBtn.addEventListener('click', buyBook)

            function buyBook(e) {
                e.target.parentElement.remove();
                let currentPrice = e.target.parentElement.children[1].textContent.split(' ')[4];
                currentPrice = Number(currentPrice)
                totalProfit += currentPrice;
                totalStoreProfit.textContent = `Total Store Profit: ${totalProfit.toFixed(2)} BGN`;

            }

            let moveBtn = document.createElement('button');
            moveBtn.textContent = `Move to old section`
            moveBtn.addEventListener('click', moveBook)

            function moveBook(e) {
                let book = e.target.parentElement;
                e.target.parentElement.remove();
                book.lastElementChild.remove();
                let discountPrice = price - price * 0.15
                book.lastElementChild.textContent = `Buy it only for ${discountPrice.toFixed(2)} BGN`
                oldBooks.appendChild(book);
            }

            div.appendChild(p);
            div.appendChild(buyBtn)

            if (year >= 2000) {
                div.appendChild(moveBtn)
                newBooks.appendChild(div);
            } else {
                oldBooks.appendChild(div);
            }
        }
    })
}