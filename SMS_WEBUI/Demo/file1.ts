// Variables and Types
let myString: string = "Hello, Sahil!";
let myNumber: number = 42;
let myBoolean: boolean = true;
console.log(myString); 
console.log(myNumber); 
console.log(myBoolean);

// Arrays
let myArray: number[] = [1, 2, 3, 4, 5];
let myStringArray: string[] = ["Vertex", "KFT", "Bebo"];

console.log(myArray);
console.log(myStringArray); 

// Objects
let myObject: { name: string; age: number; } = {
    name: "Sahil",
    age: 22,
};

console.log(myObject); 

// Functions
function hello(name: string): string {
    return `Hello, ${name}!`;
}

console.log(hello("Sahil"));

// Interfaces
interface Person {
    name: string;
    age: number;
}

function HelloPerson(person: Person): string {
    return `Hello, ${person.name}! You are ${person.age} years old.`;
}

let person: Person = { name: "Bob", age: 25 };
console.log(HelloPerson(person)); 

// Classes
class Animal {
    name: string;

    constructor(name: string) {
        this.name = name;
    }

    sound(): string {
        return "Some sound";
    }
}

class Dog extends Animal {
    sound(): string {
        return " Dog Sound!";
    }
}

let myDog = new Dog("xyz");
console.log(myDog.sound()); 

// Generics
function identity<T>(arg: T): T {
    return arg;
}

let result = identity<string>("Hello World!");
console.log(result);

// Enums
enum Direction {
    Up,
    Down,
    Left,
    Right,
}

let userDirection: Direction = Direction.Up;
console.log(userDirection); 
