function add(n) {
    var v = function (x) {
        return add(n + x);
    };

    v.valueOf = v.toString = function () {
        return n;
    };

    return v;
}

