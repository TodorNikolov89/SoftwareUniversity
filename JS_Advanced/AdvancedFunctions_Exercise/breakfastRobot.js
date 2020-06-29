function solution() {

    let ingredients = {};
    ingredients['protein'] = 0;
    ingredients['carbohydrate'] = 0;
    ingredients['fat'] = 0;
    ingredients['flavour'] = 0;

    function restock([microelement, qty]) {
        qty = Number(qty);
        ingredients[microelement] += qty;
        return 'Success';
    }

    function prepare([recipe, qty]) {
        qty = Number(qty);
        let message = '';

        switch (recipe) {
            case 'apple':
                if (ingredients['carbohydrate'] < qty) {
                    message = `Error: not enough carbohydrate in stock`;
                }

                if (ingredients['flavour'] < qty * 2) {
                    message = `Error: not enough flavour in stock`;
                }

                if (message === '') {
                    ingredients['carbohydrate'] -= qty;
                    ingredients['flavour'] -= qty * 2;
                    return "Success";
                }
                return message;
                break;
            case 'lemonade':

                if (ingredients['flavour'] < qty * 20) {
                    message = `Error: not enough flavour in stock`;
                }

                if (ingredients['carbohydrate'] < qty * 10) {
                    message = `Error: not enough carbohydrate in stock`;
                }              

                if (message === '') {
                    ingredients['carbohydrate'] -= qty * 10;
                    ingredients['flavour'] -= qty * 20;
                    return "Success";
                }
                return message;
                break;
            case 'burger':
                if (ingredients['carbohydrate'] < qty * 5) {
                    message = `Error: not enough carbohydrate in stock`;
                }

                if (ingredients['fat'] < qty * 7) {
                    message = `Error: not enough fat in stock`;
                }

                if (ingredients['flavour'] < qty * 3) {
                    message = `Error: not enough flavour in stock`;
                }

                if (message === '') {
                    ingredients['carbohydrate'] -= qty * 5;
                    ingredients['fat'] -= qty * 7;
                    ingredients['flavour'] -= qty * 3;
                    return "Success";
                }
                return message;
                break;
            case 'eggs':
                if (ingredients['protein'] < qty * 5) {
                    message = `Error: not enough protein in stock`;
                }

                if (ingredients['fat'] < qty * 1) {
                    message = `Error: not enough fat in stock`;
                }

                if (ingredients['flavour'] < qty * 1) {
                    message = `Error: not enough flavour in stock`;
                }

                if (message === '') {
                    ingredients['protein'] -= qty * 5;
                    ingredients['fat'] -= qty * 1;
                    ingredients['flavour'] -= qty * 1;
                    return "Success";
                }

                return message;
                break;
            case 'turkey':
                if (ingredients['protein'] < qty * 10) {
                    message = `Error: not enough protein in stock`;
                }

                if (ingredients['carbohydrate'] < qty * 10) {
                    message = `Error: not enough carbohydrate in stock`;
                }

                if (ingredients['fat'] < qty * 10) {
                    message = `Error: not enough fat in stock`;
                }

                if (ingredients['flavour'] < qty * 10) {
                    message = `Error: not enough flavour in stock`;
                }

                if (message === '') {
                    ingredients['protein'] -= qty * 10;
                    ingredients['carbohydrate'] -= qty * 10;
                    ingredients['fat'] -= qty * 10;
                    ingredients['flavour'] -= qty * 10;
                    return "Success";
                }

                return message;
                break;
        }
    }

    function report() {
        return `protein=${ingredients['protein']} carbohydrate=${ingredients['carbohydrate']} fat=${ingredients['fat']} flavour=${ingredients['flavour']}`;
    }

    return function (command) {
        let tokens = command.split(' ');
        let action = tokens.shift();
        switch (action) {
            case "prepare":
                return prepare(tokens);
                break;
            case "restock":
                return restock(tokens);
                break;
            case "report":
                return report();
        }
    }
}

