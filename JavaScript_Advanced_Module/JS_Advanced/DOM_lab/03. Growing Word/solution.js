function growingWord() {
  let index = 0;
  let arr = [{ styleName: "blue", fontSize: "2px" }, { styleName: "green", fontSize: "4px" }, { styleName: "red", fontSize: "8px" }];
  let ppt = 2;

  //let element = document.getElementsByClassName('conditions')[0];
  let element = document.getElementsByClassName('conditions')[0];
  let p = element.getElementsByTagName('p')[0];
  let currentColor = p.style.color;

  if (currentColor === "") {
    p.style.color = arr[index].styleName;
    p.style.fontSize = arr[index].fontSize
  } else {
    index = arr.findIndex(a => a.styleName === currentColor);
    if (index === 2) {
      index = -1;
    }

    let nextIndex = arr[++index];
    p.style.color = nextIndex.styleName;
    p.style.fontSize = nextIndex.fontSize
  }
}