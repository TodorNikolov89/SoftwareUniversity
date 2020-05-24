function solve() {
    let buttons = document.getElementsByClassName("keys")[0];
    let expression = document.getElementById('expressionOutput')
    let result = document.getElementById('resultOutput');
    let clearBtn = document.getElementsByClassName("clear")[0];

    let operators = ['+', '-', '/', '*']

    let operations = {
        '+': (num1, num2) => Number(num1) + Number(num2),
        '-': (num1, num2) => Number(num1) - Number(num2),
        '*': (num1, num2) => Number(num1) * Number(num2),
        '/': (num1, num2) => Number(num1) / Number(num2)
    }

    clearBtn.addEventListener('click', () => {
        result.innerHTML = '';
        expression.innerHTML = '';
    })

    buttons.addEventListener('click', ({ target: { value } }) => {

        if (!value) {
            return
        }

        if (value === '=') {
            let params = expression.innerHTML.split(' ').filter(x => x !== '');

            if (params[2] !== '') {
                result.innerHTML = operations[params[1]](params[0], params[2]);
                return;
            }

            result.innerHTML = 'NaN';
            return;
        }

        if (operators.includes(value)) {
            expression.innerHTML = expression.innerHTML + ` ${value} `;
            return;
        }

        expression.innerHTML = expression.innerHTML + value;
    })
}