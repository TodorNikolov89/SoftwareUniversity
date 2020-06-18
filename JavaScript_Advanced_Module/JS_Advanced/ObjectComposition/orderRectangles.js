function solve(input) {
    function createRectangle(width, height) {
        let rectangle = {
            width: width,
            height: height,
            area: function () {
                return this.width * this.height
            },
            compareTo: function (other) {
                return other.area() - rectangle.area() || other.width - rectangle.width;
            }
        }

        return rectangle;
    }

    let rectangles = [];
    for (const [width, height] of input) {
        rectangles.push(createRectangle(width, height));
    }

    rectangles.sort((a, b) => a.compareTo(b))

    return rectangles
}

console.log(solve([[10, 5], [5, 12]]))