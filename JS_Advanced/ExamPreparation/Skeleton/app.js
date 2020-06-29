function solve() {
    let addSection = document.getElementsByTagName('section')[0]
    let openSection = document.getElementsByTagName('section')[1]
    let inProgressSection = document.getElementsByTagName('section')[2]
    let completeSection = document.getElementsByTagName('section')[3]

    let addBtn = document.getElementById('add');
    addBtn.addEventListener('click', (e) => {
        e.preventDefault();
        let taskContent = document.getElementById('task');
        let taskDescription = document.getElementById('description')
        let taskDate = document.getElementById('date');

        if (taskContent.value === "" || taskDescription === "" || taskDate === "") {
            return;
        }

        let newArticle = document.createElement('article');
        newArticle.innerHTML = `<h3>${taskContent.value}</h3>\n
                                <p>Description: ${taskDescription.value}</p>\n
                                <p>Due Date: ${taskDate.value}</p>\n`

        let div = document.createElement('div');
        div.className = 'flex';
        let startBtn = document.createElement('button');
        startBtn.className = 'green'
        startBtn.textContent = "Start"
        let deleteBtn = document.createElement('button');
        deleteBtn.className = 'red'
        deleteBtn.textContent = 'Delete'
        startBtn.addEventListener('click', (e) => {
            let article = e.target.parentElement.parentElement;
            e.target.parentElement.parentElement.remove();
            let deleteBtn = article.lastElementChild.children[0];
            let finishBtn = article.lastElementChild.children[1];
            deleteBtn.className = 'red';
            deleteBtn.textContent = 'Delete';
            finishBtn.className = 'orange';
            finishBtn.textContent = 'Finish';
            deleteBtn.addEventListener('click', deleteArticle)
            finishBtn.addEventListener('click', finishArticle)
            inProgressSection.children[1].appendChild(article)
        })

        deleteBtn.addEventListener('click', deleteArticle);

        function deleteArticle(e) {
            e.target.parentElement.parentElement.remove();
        }

        function finishArticle(e) {
            let finishedArticle = e.target.parentElement.parentElement;
            finishedArticle.lastElementChild.remove();
            completeSection.lastElementChild.appendChild(finishedArticle)
        }

        div.appendChild(startBtn);
        div.appendChild(deleteBtn);
        newArticle.appendChild(div)

        openSection.children[1].appendChild(newArticle)
    })
}