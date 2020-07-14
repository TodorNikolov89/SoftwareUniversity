function loadCommits() {
    let username = document.querySelector('input#username').value;
    let repository = document.querySelector('input#repo').value;
    let commits = document.querySelector('#commits')
    commits.innerHTML = ''
    let url = `https://api.github.com/repos/${username}/${repository}/commits`;

    fetch(url)
        .then((responce) => {
            if (!responce.ok) {
                throw new Error(`${responce.status} (${responce.statusText})`)
            }

            return responce.json()
        })
        .then((responce) => {
            for (const c of responce) {
                let liElement = document.createElement('li');
                liElement.textContent = `${c.commit.author.name}: ${c.commit.message}`
                commits.appendChild(liElement)
            }
        })
        .catch((e) => {
            let liElement = document.createElement('li');
            liElement.textContent = `${e}`
            commits.appendChild(liElement)
        })
}