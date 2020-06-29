function addAandRemoveElements(input) {
    let arr = [];
    let number = 0;
    for (const command of input) {
        number++;
        if (command === 'add') {
            arr.push(number);
        }

        if (command === 'remove') {
            if (arr.length > 0) {
                arr.pop();
            }
        }
    }

    if (arr.length > 0){
        arr.forEach(element => {
            console.log(element);
        });
    } else {
        console.log("Empty");
    } 
}

// addAandRemoveElements(['add', 
// 'add', 
// 'remove', 
// 'add', 
// 'add']
// )