const appId = 'BE094FA4-9927-FF29-FFE7-C8418A87A000';
const apiKey = '38C52B57-0394-4346-B33D-090027C5D175';

function host(endpoint) {
    return `https://api.backendless.com/${appId}/${apiKey}/${endpoint}`;
}

const endpoitns = {
    REGISTER: 'users/register',
    LOGIN: 'users/login',
    TEAMS: 'data/teams',
    UPDATE_USER: 'users/'
}

export async function register(username, password) {
    return await (await fetch(host(endpoitns.REGISTER), {
        method: 'POST',
        headers: {
            'ContentType': 'application/json'
        },
        body: JSON.stringify({
            username,
            password
        })
    })).json();
}

export async function login(username, password) {
    return await (await fetch(host(endpoitns.LOGIN), {
        method: 'POST',
        headers: {
            'ContentType': 'application/json'
        },
        body: JSON.stringify({
            login: username,
            password
        })
    })).json();
}

async function setUserTeamId(userId, teamId) {
    const token = localStorage.getItem('userToken');
    if (!token) {
        throw new Error('User is not logged in!')
    }

    return (await fetch(host(endpoitns.UPDATE_USER + userId), {
        method: 'PUT',
        headers: {
            'ContentType': 'application/json',
            'user-token': token
        },
        body: JSON.stringify({
            teamId
        })
    })).json();
}

export async function createTeam(team) {

    const token = localStorage.getItem('userToken');
    if (!token) {
        throw new Error('User is not logged in!')
    }

    const result = await (await fetch(host(endpoitns.TEAMS), {
        method: 'POST',
        headers: {
            'ContentType': 'application/json',
            'user-token': token
        },
        body: JSON.stringify(team)
    })).json();

    if (result.hasOwnProperty('errorData')) {
        const error = new Error()
        Object.assign(error, result)
        throw error
    }

    const userUpdateResult = await setUserTeamId(result.ownerId, result.objectId)


    if (userUpdateResult.hasOwnProperty('errorData')) {
        const error = new Error()
        Object.assign(error, userUpdateResult)
        throw error
    }
    return result
}

export async function getTeamsById(id) {
    return (await fetch(host(endpoitns.TEAMS + '/' + id))).json()
}



export async function getTeams() {
    return (await fetch(host(endpoitns.TEAMS ))).json()
}