function encodeAndDecodeMessages() {
    let encodeBtn = document.getElementsByTagName('button')[0]
    let decodeBtn = document.getElementsByTagName('button')[1]
    let senderTextArea = document.querySelectorAll('textarea')[0]
    let receiverTextArea = document.querySelectorAll('textarea')[1]

    encodeBtn.addEventListener('click', () => {
        let message = senderTextArea.value;
        let encodedMessage = '';

        for (let i = 0; i < message.length; i++) {
            encodedMessage += String.fromCharCode(getAscii(message[i]) + 1)
        }

        senderTextArea.value = "";
        receiverTextArea.value = encodedMessage;

    })

    decodeBtn.addEventListener('click', () => {
        let message = receiverTextArea.value;
        let decodedMessage = '';

        for (let i = 0; i < message.length; i++) {
            decodedMessage += String.fromCharCode(getAscii(message[i]) - 1)
        }

        receiverTextArea.value = decodedMessage;
    })

    function getAscii(ch) {
        return ch.charCodeAt(0)
    }
}