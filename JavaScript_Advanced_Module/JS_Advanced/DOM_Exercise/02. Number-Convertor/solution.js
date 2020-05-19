function solve() {
    let optionList = document.querySelectorAll("#selectMenuTo")[0];
    let convertButton = document.querySelector('button');
    let num = document.querySelector('#input');

    let mathObj = {
        binary: (number) => number.toString(2),
        hexadecimal: (number) => number.toString(16).toUpperCase(),
    }

    optionList.innerHTML = `    
    <option selected value=""></option>
    <option value="hexadecimal">Hexadecimal</option>
    <option value="binary">Binary</option>
    `
    convertButton.addEventListener('click', () => {
        document.getElementById('result').value = mathObj[optionList.value](Number(num.value))
    })
}