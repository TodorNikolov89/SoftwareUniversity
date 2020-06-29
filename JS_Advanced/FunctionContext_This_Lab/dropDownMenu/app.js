function solve() {
    let dropDownMenuBtn = document.getElementById('dropdown');
    let showClr = document.getElementById('dropdown-ul');
    let div = document.getElementById('box');
    let divClr = div.style.backgroundColor;
    let textClr = div.style.color;

    dropDownMenuBtn.addEventListener('click', () => {

        if (showClr.style.display === 'none' || showClr.style.display === '') {
            showClr.style.display = 'block';
        } else {
            showClr.style.display = 'none'
            div.style.backgroundColor = divClr;
            div.style.color = textClr;
        }
    })

    showClr.addEventListener('click', (e) => {
        let color = e.target.innerText;
        div.style.backgroundColor = color;
        div.style.color = 'black'
    })
}