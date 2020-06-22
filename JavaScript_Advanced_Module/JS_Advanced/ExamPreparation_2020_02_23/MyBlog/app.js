function solve() {
   let createBtn = document.getElementsByClassName('btn create')[0];
   let articlesSection = document.getElementsByTagName('section')[1];
   let archivedArticlesSection = document.getElementsByClassName('archive-section')[0].lastElementChild;

   createBtn.addEventListener('click', (e) => {
      e.preventDefault();
      let author = document.getElementById('creator');
      let title = document.getElementById('title');
      let category = document.getElementById('category');
      let content = document.getElementById('content');

      let article = document.createElement('article');

      article.innerHTML = `<h1>${title.value}</h1>
      <p>Category:<strong>${category.value}</strong></p>
      <p>Creator:<strong>${author.value}</strong></p>
      <p>${content.value}</p>`

      let div = document.createElement('div');
      div.className = "buttons";

      let deleteBtn = document.createElement('button');
      deleteBtn.textContent = "Delete"
      deleteBtn.className = "btn delete"

      deleteBtn.addEventListener('click', deteleArticle)

      function deteleArticle(e) {
         let articleToBeDeleted = e.target.parentElement.parentElement;
         articleToBeDeleted.remove();
      }

      let archiveBtn = document.createElement('button');
      archiveBtn.className = "btn archive"
      archiveBtn.textContent = "Archive"
      archiveBtn.addEventListener('click', archiveArticle)

      function archiveArticle(e) {
         console.log(e)
         let title = e.target.parentElement.parentElement.children[0].textContent;
         e.target.parentElement.parentElement.remove();
         title = title.replace(/\s/gm, "")
         let li = document.createElement('li');
         li.textContent = title;
         archivedArticlesSection.appendChild(li);

         Array.from(archivedArticlesSection.getElementsByTagName("li"))
            .sort((a, b) => a.textContent.localeCompare(b.textContent))
            .forEach(li => archivedArticlesSection.appendChild(li));
      }

      div.appendChild(deleteBtn);
      div.appendChild(archiveBtn);

      article.appendChild(div)
      articlesSection.appendChild(article);
   })
}
