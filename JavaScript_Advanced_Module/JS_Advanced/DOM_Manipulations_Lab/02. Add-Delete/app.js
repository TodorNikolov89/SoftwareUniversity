function addItem() {
    let text = document.getElementById('newText').value + " ";
    let liElement = document.createElement('li');
    let list = document.getElementById('items');
    let a = document.createElement('a');
    a.textContent = "[Delete]";
    a.href = '#';
    a.addEventListener('click', deleteItem);
    liElement.innerText = text
    liElement.appendChild(a)
    list.appendChild(liElement);

    document.getElementById('newText').value = "";

    function deleteItem() {
        let li = this.parentNode;
        let ul = this.parentNode.parentNode;
        ul.removeChild(li);
    }
}