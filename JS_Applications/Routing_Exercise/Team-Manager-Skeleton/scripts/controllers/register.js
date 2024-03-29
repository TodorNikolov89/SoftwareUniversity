import { register } from '../data.js'


export default async function (context) {
    //Partials are loaded with LOAD
    this.partials = {
        header: await this.load('./templates/common/header.hbs'),
        footer: await this.load('./templates/common/footer.hbs'),
        registerForm: await this.load('./templates/register/registerForm.hbs'),
    };
    this.partial('./templates/register/registerPage.hbs')
}

export async function registerPost() {

    if (this.params.password !== this.params.repeatPassword) {
        alert("Passwords don't match!")
        return;
    }

    try {
        const result = await register(this.params.username, this.params.password)
        if (result.hasOwnProperty('errorData')) {
            const error = new Error()
            Object.assign(error, result)
            throw error
        }
        this.redirect('#/login');
    } catch (err) {
        console.error(err);
        alert(err.message)
    }
}