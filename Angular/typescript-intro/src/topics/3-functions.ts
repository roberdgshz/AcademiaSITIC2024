function addNumbers(a: number, b: number): number {
    return a + b;
}

const addNumbersArrow = (a: number, b: number): string => {
    // return (a + b).toString();
    return `${a + b}`;
}

function multiply(firstNumber: number, secondNumber?: number, base: number = 2): number{
    return firstNumber * base;
}

const multiplyResult: number = multiply(5);
const multiplyResult2: number = multiply(5, 3);
const multiplyResult3: number = multiply(5, null, 3);

console.log({multiplyResult, multiplyResult2, multiplyResult3});

const result = addNumbers(1, 2);
const result2 = addNumbersArrow(1, 2);

console.log({ result, result2 });
console.log(result, result2);
console.log('result', result)
console.log('result2', result2)

export {};