function createArticle() {
	let titleText = document.getElementById("createTitle");
	let contentText = document.getElementById("createContent");

	if (titleText.value.length !== 0 && contentText.value.length !== 0) {
		let h = document.createElement("h3");
		h.textContent = titleText.value;;

		let p = document.createElement("p");
		p.style.wordBreak = "break-all"
		p.textContent = contentText.value;

		let newArticle = document.createElement("article");
		newArticle.appendChild(h);
		newArticle.appendChild(p)

		let allArticles = document.getElementById("articles")
		allArticles.appendChild(newArticle)

		titleText.value = "";
		contentText.value = "";
	}
}