const appId = '0A3092E3-587E-B6F7-FF4C-C25EF7B6D700';
const apiKey = 'DEA9F2DB-5A27-485F-9EDD-B1A7210D59FF'

function host(endpoints) {
    return `https://api.backendless.com/${appId}/${apiKey}/data/${endpoints}`
}

export async function getStudents() {
    const response = await fetch(host('students'))
    const data = await response.json();
    data.sort((a, b) => a.IDNumber - b.IDNumber)
    return data;
}

export async function createStudent(student) {
    console.log(typeof student)
    console.log(host('students'))
    const response = await fetch(host('students'), {
        method: 'POST',
        body: JSON.stringify(student),
        headers: {
            'Content-Type': 'application/json'
        },
    });

    const data = await response.json();
    return data;
}

export async function deleteStudent(id) {
    const response = await fetch(host('students/' + id), {
        method: 'DELETE'
    });

    const data = await response.json();
    return data;
}

export async function isStudentExists(id) {
    const response = await fetch(host('students' + `/${id}`))
    const data = await response.json();
    return data;
}