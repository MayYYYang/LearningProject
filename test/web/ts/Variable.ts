function greeter(person, isman) {
    let result = "Hello, " + person;
    if (isman)
    {
        result += "(Men)";
    }
    else 
    {
        result += "(Woman)";
    }

    return result;
}

var user = "Jane User";

let isMan: boolean = false;

document.body.innerHTML = greeter(user, isMan); 

tuple();


function tuple()
{
    //
    let tuple1: [string, string,number];
    tuple1 = ["dd","dd",23];
    document.body.innerHTML += "<br/>" + tuple1[0] + "   " + tuple1[1] + "   " + tuple1[2] + "   ";
    document.body.innerHTML += "<br/>" + tuple1[5] ;

}


function enumTest() {
    enum Color { Red, Green, Blue };
    let c: Color = Color.Green;


    enum Color1 { Red = 1, Green, Blue };
    let b: Color1 = Color1.Green;

    enum Color2 { Red = 1, Green = 2, Blue = 4 };
    let c2: Color2 = Color2.Green;

}

let myAdd: (baseValue: number, increment: number) => number =
    function (x: number, y: number): number { return x + y; };

myAdd = function () { return 3; };
let a=myAdd(3,5);
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


let deck = {
    suits: ["hearts", "spades", "clubs", "diamonds"],
    cards: Array(52),
    createCardPicker: function () {
        // NOTE: the line below is now an arrow function, allowing us to capture 'this' right here
        return () => {
            let pickedCard = Math.floor(Math.random() * 52);
            let pickedSuit = Math.floor(pickedCard / 13);

            return { suit: this.suits[pickedSuit], card: pickedCard % 13 };
        }
    },
    cardPicker: function () {
        let pickedCard = Math.floor(Math.random() * 52);
        let pickedSuit = Math.floor(pickedCard / 13);

        return { suit: this.suits[pickedSuit], card: pickedCard % 13 };
    }
}

let cardPicker = deck.createCardPicker();
let pickedCard = cardPicker();
let cardPicker1 = deck.cardPicker();

document.body.innerHTML += "<br/>" + "card: " + cardPicker1.card + " of " + cardPicker1.suit;
