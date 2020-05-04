function systemComponents(input) {
    let systemMap = new Map();

    for (let row of input) {
        let line = row.split(" | ");
        let systemName = line[0];
        let componentName = line[1];
        let subcomponentName = line[2];

        if (!systemMap.get(systemName)) {
            let componentsMap = new Map();
            systemMap.set(systemName, componentsMap)
        }

        if (!systemMap.get(systemName).get(componentName)) {
            let set = [];
            systemMap.get(systemName).set(componentName, set);
        }

        systemMap.get(systemName).get(componentName).push(subcomponentName)
    }

    let sortedSystem = Array.from(systemMap.keys()).sort((s1, s2) => sortSystems(s1, s2))

    for (let system of sortedSystem) {
        console.log(system)
        let sortedComponents = Array.from(systemMap.get(system).keys()).sort((c1, c2) => sortComponents(system, c1, c2));
        for (let component of sortedComponents) {
            console.log(`|||${component}`)
            systemMap.get(system).get(component).forEach(subComponenet => {
                console.log(`||||||${subComponenet}`)
            });
        }
    }
    function sortSystems(s1, s2) {
        if (systemMap.get(s1).size != systemMap.get(s2).size) {
            return systemMap.get(s2).size - systemMap.get(s1).size;
        } else {
            return s1.toLowerCase().localeCompare(s2.toLowerCase());
        }
    }

    function sortComponents(system, c1, c2) {
        return systemMap.get(system).get(c2).length - systemMap.get(system).get(c1).length;
    }
}

systemComponents([
    'SULS | Main Site | Home Page',
    'SULS | Main Site | Login Page',
    'SULS | Main Site | Register Page',
    'SULS | Judge Site | Login Page',
    'SULS | Judge Site | Submittion Page',
    'Lambda | CoreA | A23',
    'SULS | Digital Site | Login Page',
    'Lambda | CoreB | B24',
    'Lambda | CoreA | A24',
    'Lambda | CoreA | A25',
    'Lambda | CoreC | C4',
    'Indice | Session | Default Storage',
    'Indice | Session | Default Security'
])