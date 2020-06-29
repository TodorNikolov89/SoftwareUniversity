function solve() {
   let tbody = document.getElementsByTagName('tbody')[0].children
   let rows = document.getElementsByTagName('tbody')[0].children


   Array.from(tbody).forEach(element => {
      element.addEventListener('click', changeStyle)
   })

   function changeStyle(e) {
      let ref = e.target.parentNode;
      Array.from(rows).forEach(x => {
         console.log(x)
         console.log(ref)
         if (x !== ref) {
            x.style.cssText = "";
         }
      })
      ref.style.cssText = ref.style.cssText ? '' : 'background-color:rgb(65, 63, 94)'
   }
}
