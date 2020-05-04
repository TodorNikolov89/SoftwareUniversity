function usernames(input) {
    let names = new Set();

    for (let name of input) {
        names.add(name);
    }

    let sortedNames = Array.from(names).sort((n1, n2) => getSortedNames(n1, n2));

    sortedNames.forEach(element => {
        console.log(element);
    });

    function getSortedNames(n1, n2) {
        if (n1.length != n2.length) {
            return n1.length - n2.length;
        } else {
            return n1.localeCompare(n2)
        }
    }
}


usernames([
    'Ashton',
    'Kutcher',
    'Ariel',
    'Lilly',
    'Keyden',
    'Aizen',
    'Billy',
    'Braston'
])