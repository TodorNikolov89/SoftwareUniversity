import { login } from '../data.js'

export default async function (context) {
    this.partials = {
        header: await this.load('./templates/common/header.hbs'),
        footer: await this.load('./templates/common/footer.hbs'),
        loginForm: await this.load('./templates/login/loginForm.hbs')
    };
    this.partial('./templates/login/loginPage.hbs')
}

export async function loginPost() {
    try {
        const result = await login(this.params.username, this.params.password)
        if (result.hasOwnProperty('errorData')) {
            const error = new Error()
            Object.assign(error, result)
            throw error
        }

        this.app.userData.loggedIn = true;
        this.app.userData.username = result.username;
        this.app.userData.userId = result.userId;

        localStorage.setItem('userToken', result['user-token'])
        localStorage.setItem('userName', result.username) 
        localStorage.setItem('userName', result.objectId) 

        this.redirect('#/home');
    } catch (err) {
        console.error(err);
        alert(err.message)
    }
}