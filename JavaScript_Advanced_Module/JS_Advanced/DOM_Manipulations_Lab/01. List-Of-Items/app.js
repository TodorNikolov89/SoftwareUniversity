function addItem() {
    let text = document.getElementById('newItemText').value;
    let liElement = document.createElement('li');
    liElement.innerText = text;
    let list = document.getElementById('items');
    list.appendChild(liElement);
}