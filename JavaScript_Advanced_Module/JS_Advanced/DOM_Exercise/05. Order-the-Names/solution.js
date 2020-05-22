function solve() {
    let addButton = document.getElementsByTagName('button')[0]
    let name = document.getElementsByTagName('input')[0].value;
    addButton.addEventListener('click', () => {        
        let ch = name[0].toUpperCase();
        name = name.slice(1, name.length).toLowerCase()
        let i = ch.charCodeAt(0)
        let row = i - 65;
        let list = document.getElementsByTagName("ol")[0];
        let currentContent = list.children[row].innerHTML;
        name = ch + name;
        if (currentContent === "") {
            currentContent += name;
        } else {
            currentContent += `, ${name}`;
        }

        list.children[row].innerHTML = currentContent;
        document.getElementsByTagName('input')[0].value = "";
    })
}