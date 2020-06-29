function equalNeighbors(input) {
    let counter = 0;
    let elements = [];
    for (let row = 0; row < input.length; row++) {
        let currentRow = input[row];
        for (let col = 0; col < currentRow.length; col++) {
            let currentElement = input[row][col];
            let newElement;


            if (row - 1 >= 0) {
                newElement = input[row - 1][col];
                if (compareElements(currentElement, newElement, elements)) {
                    elements.push(newElement);
                    counter++;
                }
            }

            if (row + 1 <= input.length - 1) {
                newElement = input[row + 1][col];
                if (compareElements(currentElement, newElement, elements)) {
                    elements.push(newElement);
                    counter++;
                }
            }

            if (col - 1 >= 0) {
                newElement = input[row][col - 1];
                if (compareElements(currentElement, newElement, elements)) {
                    elements.push(newElement);
                    counter++;
                }
            }

            if (col + 1 <= currentRow.length - 1) {
                newElement = input[row][col + 1];
                if (compareElements(currentElement, newElement, elements)) {
                    elements.push(newElement);
                    counter++;
                }
            }

        }
    }

    console.log(counter)

    function compareElements(currentElement, newElement, elements) {

        if (currentElement === newElement && !elements.includes(newElement)) {
            return true;
        } else {
            return false;
        }
    }

}

equalNeighbors([['2', '3', '4', '7', '0'],
['4', '0', '5', '3', '4'],
['2', '3', '5', '4', '2'],
['9', '8', '7', '5', '4']]
)