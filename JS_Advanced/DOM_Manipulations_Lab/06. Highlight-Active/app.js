function focus() {
    let allDivs = document.getElementsByTagName('div')[0].children;

    for (let i = 0; i < allDivs.length; i++) {
        allDivs[i].lastElementChild.addEventListener('focus', makeFocused);
    }

    function makeFocused() {
        let currentDiv = this.parentElement;

        for (let i = 0; i < allDivs.length; i++) {
            if (allDivs[i] !== currentDiv) {
                allDivs[i].classList.remove('focused');
            }
        }

        currentDiv.classList.add('focused');
    }
}