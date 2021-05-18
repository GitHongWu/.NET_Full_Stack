// 1
let salaries = {
    John: 100,
    Ann: 160,
    Pete: 130
}

let sum = function (salaries) {
    let sum = 0
    for(var key in salaries) {
        var value = salaries[key];
        sum += value 
    }
    return sum
}

console.log(sum(salaries))

// 2
let menu = {
    width: 200,
    height: 300,
    title: "My menu"
};

multiplyNumeric = function (menu) {
    for(var key in menu) {
        var value = menu[key]
        if (!isNaN(value)) {
            menu[key] = value * 2
        }
    }
}

multiplyNumeric(menu);
console.log(menu)

// 3
function ValidateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}
console.log(ValidateEmail("1@w.com"))
console.log(ValidateEmail("1@.com"))

// 4
function truncate(str, maxlength) {
    if (str.length > maxlength) {
        return (str.substr(0, 19)+'...')
    }
    return str
}

let str = "What I'd like to tell on this topic is:"
console.log(truncate(str, 20))
str = "Hi everyone!"
console.log(truncate(str, 20))

// 5
let array = ["James", "Brennie"]
array.push("Robert")
array[Math.floor(array.length/2)] = "Calvin"
array.shift()
array.unshift("Rose")
array.unshift("Regal")
console.log(array)

// 6
function validateCards(str) {

    let Card = {
        "Card": "",
        "isValid": false,
        "isAllowed": false,
    }
    Card.Card = str

    arrayInt = str.split('').map(x => +x)
    arrayInt = arrayInt.slice(0, arrayInt.length - 1);
    arrayInt = arrayInt.map(x => x * 2)
    sum = arrayInt.reduce((a, b) => a + b, 0)
    if (sum % 10 == str.charAt(str.length - 1)) {
        Card.isValid = true;
    }
    return Card
}

console.log(validateCards("6724843711060148"))

// 7
function ValidateEmail(email) {
    var re = /^[a-z]{1,6}_?[0-9]{0,4}@hackerrank.com/
    return re.test(email);
}
console.log(ValidateEmail("julia@hackerrank.com"))
console.log(ValidateEmail("julia_@hackerrank.com"))
console.log(ValidateEmail("julia_0@hackerrank.com"))
console.log(ValidateEmail("julia0_@hackerrank.com"))
console.log(ValidateEmail("julia@gmail.com"))