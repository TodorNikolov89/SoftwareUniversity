const reposEl = document.getElementById('repos');
const usernameEl = document.getElementById('username')

function loadRepos() {
	reposEl.innerHTML = `<h1>Loading...</h1>`
	const username = usernameEl.value;
	const url = `https://api.github.com/users/${username}/repos`
	fetch(url)
		.then(res => res.json())
		.then(data => {
			data.forEach(item => {
				const li = document.createElement('li');
				const a = document.createElement('a')
				a.href = item.html_url;
				a.innerHTML = item.full_name;
				li.appendChild(a);
				reposEl.appendChild(li);
			});
		})
}