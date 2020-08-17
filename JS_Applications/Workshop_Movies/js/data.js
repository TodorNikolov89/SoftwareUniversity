const appId = '966E9217-6A5F-02AF-FF05-AD3D90DFEB00';
const apiKey = '67F52A9E-5E23-4909-BC48-53D7722B1AB9'

function host(endpoint) {
    return `https://api.backendless.com/${appId}/${apiKey}/${endpoint}`
}

const endpoints = {
    LOGIN: 'users/login',
    LOGOUT: 'users/logout',
    REGISTER: 'users/register',
    LOGIN: 'users/login',
    MOVIES: "data/movies",
    EDIT: "data/movies",
    DELETE: "data/movies",
    USERMOVIES: "data/movies"
}

async function register(username, password) {
    return (await fetch(host(endpoints.REGISTER), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username,
            password
        })
    })).json()
}

async function login(username, password) {
    const result = await (await fetch(host(endpoints.LOGIN), {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            login: username,
            password
        })
    })).json();

    localStorage.setItem('userToken', result['user-token'])
    localStorage.setItem('username', result.username)
    localStorage.setItem('userId', result.objectId);

    return result;
}

async function logout() {
    let token = localStorage.getItem('userToken');

    return (await fetch(host(endpoints.LOGOUT), {
        method: 'GET',
        headers: {
            "user-token": token
        }
    }))
}

async function addMovie(movie) {
    let token = localStorage.getItem('userToken');

    const result = await (await fetch(host(endpoints.MOVIES), {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'user-token': token
        },
        body: JSON.stringify(movie)
    })).json();

    localStorage.setItem('userId', result.objectId);

    return result;
}

async function editMovie(id, updatedProps) {
    let token = localStorage.getItem('userToken');

    const result = await (await fetch(host(endpoints.EDIT + `/${id}`), {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
            'user-token': token
        },
        body: JSON.stringify(updatedProps)
    })).json();

    return result;
}

async function deleteMovie(id) {
    let token = localStorage.getItem('userToken');

    const result = await (await fetch(host(endpoints.DELETE + `/${id}`), {
        method: "DELETE",
        headers: {
            'user-token': token
        }
    })).json();

    return result;
}

async function getUserMovies() {
    let token = localStorage.getItem('userToken');
    const userId = localStorage.getItem('userId')
    let clause = escape(`where=ownerId=${userId}`)
    return (await fetch(host(endpoints.USERMOVIES + `?${clause}`), {
        method: 'GET',
        headers: {
            "user-token": token
        }
    })).json();
}