function validate() {
    let input = document.getElementById('email');

    input.addEventListener('change', checkEmail);

    function checkEmail(mail) {
        let pattertn = /(.+)@(.+){2,}\.(.+){2,}/;
        if (!pattertn.test(mail.target.value)) {
            document.getElementById('email').className = 'error'
        } else {
            document.getElementById('email').className = '';
        }
    }
}