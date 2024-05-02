"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
// Variables and Types
var myString = "Hello, Sahil!";
var myNumber = 42;
var myBoolean = true;
console.log(myString);
console.log(myNumber);
console.log(myBoolean);
// Arrays
var myArray = [1, 2, 3, 4, 5];
var myStringArray = ["Vertex", "KFT", "Bebo"];
console.log(myArray);
console.log(myStringArray);
// Objects
var myObject = {
    name: "Sahil",
    age: 22,
};
console.log(myObject);
// Functions
function hello(name) {
    return "Hello, " + name + "!";
}
console.log(hello("Sahil"));
function HelloPerson(person) {
    return "Hello, " + person.name + "! You are " + person.age + " years old.";
}
var person = { name: "Bob", age: 25 };
console.log(HelloPerson(person));
// Classes
var Animal = /** @class */ (function () {
    function Animal(name) {
        this.name = name;
    }
    Animal.prototype.sound = function () {
        return "Some sound";
    };
    return Animal;
}());
var Dog = /** @class */ (function (_super) {
    __extends(Dog, _super);
    function Dog() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Dog.prototype.sound = function () {
        return " Dog Sound!";
    };
    return Dog;
}(Animal));
var myDog = new Dog("xyz");
console.log(myDog.sound());
// Generics
function identity(arg) {
    return arg;
}
var result = identity("Hello World!");
console.log(result);
// Enums
var Direction;
(function (Direction) {
    Direction[Direction["Up"] = 0] = "Up";
    Direction[Direction["Down"] = 1] = "Down";
    Direction[Direction["Left"] = 2] = "Left";
    Direction[Direction["Right"] = 3] = "Right";
})(Direction || (Direction = {}));
var userDirection = Direction.Up;
console.log(userDirection);
console.log("hello");
