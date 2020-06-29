(function solve() {
    Array.prototype.last = function () {
        return this[this.length - 1];
    }

    Array.prototype.skip = function (n) {
        let arr = [];
        arr = this.slice(Number(n))
        return arr;
    }


    Array.prototype.take = function (n) {
        let arr = [];
        arr = this.slice(0, Number(n))
        return arr;
    }

    Array.prototype.sum = function () {
        let sum = this.reduce((acc, curr) => acc + curr)
        return sum;
    }

    Array.prototype.average = function () {
        let nums = this.length;
        let aver = this.reduce((acc, curr) => acc + curr) / nums;
        return aver;
    }
})();
