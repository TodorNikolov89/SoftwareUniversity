function notify(message) {
    let div = document.getElementById('notification');
    div.innerHTML = message;
    div.style.display = 'block';
    setTimeout(hideNotificationBox, 2000);

    function hideNotificationBox() {
        div.style.display = 'none';
    }
}