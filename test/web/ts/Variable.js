function greeter(person, isman) {
    var result = "Hello, " + person;
    if (isman) {
        result += "(Men)";
    }
    else {
        result += "(Woman)";
    }
    return result;
}
var user = "Jane User";
var isMan = false;
document.body.innerHTML = greeter(user, isMan);
tuple();
function tuple() {
    //
    var tuple1;
    tuple1 = ["dd", "dd", 23];
    document.body.innerHTML += "<br/>" + tuple1[0] + "   " + tuple1[1] + "   " + tuple1[2] + "   ";
    document.body.innerHTML += "<br/>" + tuple1[5];
}
function enumTest() {
    var Color;
    (function (Color) {
        Color[Color["Red"] = 0] = "Red";
        Color[Color["Green"] = 1] = "Green";
        Color[Color["Blue"] = 2] = "Blue";
    })(Color || (Color = {}));
    ;
    var c = Color.Green;
    var Color1;
    (function (Color1) {
        Color1[Color1["Red"] = 1] = "Red";
        Color1[Color1["Green"] = 2] = "Green";
        Color1[Color1["Blue"] = 3] = "Blue";
    })(Color1 || (Color1 = {}));
    ;
    var b = Color1.Green;
    var Color2;
    (function (Color2) {
        Color2[Color2["Red"] = 1] = "Red";
        Color2[Color2["Green"] = 2] = "Green";
        Color2[Color2["Blue"] = 4] = "Blue";
    })(Color2 || (Color2 = {}));
    ;
    var c2 = Color2.Green;
}
var myAdd = function (x, y) { return x + y; };
myAdd = function () { return 3; };
var a = myAdd(3, 5);
document.body.innerHTML += "<br/>" + a;
//let deck = {
//    suits: ["hearts", "spades", "clubs", "diamonds"],
//    cards: Array(52),
//    createCardPicker: function () {
//        return function () {
//            let pickedCard = Math.floor(Math.random() * 52);
//            let pickedSuit = Math.floor(pickedCard / 13);
//            return { suit: this.suits[pickedSuit], card: pickedCard % 13 };
//        }
//    }
//}
//let cardPicker = deck.createCardPicker();
//let pickedCard = cardPicker();
//document.body.innerHTML += "<br/>" + "card: " + pickedCard.card + " of " + pickedCard.suit;
var deck = {
    suits: ["hearts", "spades", "clubs", "diamonds"],
    cards: Array(52),
    createCardPicker: function () {
        var _this = this;
        // NOTE: the line below is now an arrow function, allowing us to capture 'this' right here
        return function () {
            var pickedCard = Math.floor(Math.random() * 52);
            var pickedSuit = Math.floor(pickedCard / 13);
            return { suit: _this.suits[pickedSuit], card: pickedCard % 13 };
        };
    },
    cardPicker: function () {
        var pickedCard = Math.floor(Math.random() * 52);
        var pickedSuit = Math.floor(pickedCard / 13);
        return { suit: this.suits[pickedSuit], card: pickedCard % 13 };
    }
};
var cardPicker = deck.createCardPicker();
var pickedCard = cardPicker();
var cardPicker1 = deck.cardPicker();
document.body.innerHTML += "<br/>" + "card: " + cardPicker1.card + " of " + cardPicker1.suit;
//# sourceMappingURL=Variable.js.map