export default async function (context) {
    //Partials are loaded with LOAD

    this.partials = {
        header: await this.load('./templates/common/header.hbs'),
        footer: await this.load('./templates/common/footer.hbs'),
        editForm: await this.load('./templates/edit/editForm.hbs')
    };

    this.partial('./templates/edit/editPage.hbs')
}