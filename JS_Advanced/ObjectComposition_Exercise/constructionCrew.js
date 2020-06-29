function solve(workerObject) {

    let instance = workerObject;

    if (instance.dizziness === true) {
        instance.levelOfHydrated += 0.1 * Number(instance.weight) * Number(instance.experience);
        instance.dizziness = false;
    }

    return workerObject
}


solve({
    weight: 80,
    experience: 1,
    levelOfHydrated: 0,
    dizziness: true
})