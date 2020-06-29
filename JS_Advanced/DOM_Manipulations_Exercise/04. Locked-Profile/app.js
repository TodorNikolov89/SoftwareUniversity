function lockedProfile() {
    let btns = document.querySelectorAll('button');

    for (const b of btns) {
        b.addEventListener('click', showHideInformation)
    }

    function showHideInformation(event) {
        let btn = event.target;
        let profile = btn.parentNode;
        let lockButton = profile.querySelector('input[type="radio"]:checked').value;
        let hiddenDiv = profile.querySelectorAll('div')[0];

        if (lockButton === 'unlock') {
            if (btn.textContent === 'Hide it') {
                hiddenDiv.style.display = 'none'
                btn.textContent = 'Show more';
            } else {
                btn.textContent = 'Show more'
                hiddenDiv.style.display = 'block'
                btn.textContent = 'Hide it';
            }
        }
    }
}