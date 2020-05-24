function solve() {
   let sendButton = document.getElementById("send");

   sendButton.addEventListener('click', function () {
      let textAreaContent = document.getElementById("chat_input").value;
      let newMessage = document.createElement("div");
      newMessage.className = "message my-message";
      newMessage.textContent = textAreaContent;
      newMessage.style.wordBreak="break-all"

      let messageBox = document.getElementById("chat_messages");
      messageBox.appendChild(newMessage);
      document.getElementById("chat_input").value = "";
   })
}