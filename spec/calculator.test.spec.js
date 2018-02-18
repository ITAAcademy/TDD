const Calculator = require('../Calculator.js');

describe("Is it password correct?", function () {

  let calculator = new Calculator;

  it("The method can take two numbers, and will return their sum", function () {
    expect(calculator.add('1,2')).toBe(3);
  });

  it("The method can take one number, and will return their sum", function () {
    expect(calculator.add('1')).toBe(1);
  });

  it("The method can take zero numbers, and will return their sum", function () {
    expect(calculator.add('')).toBe(0);
  });

  it("Allow the Add method to handle an unknown amount of numbers", function () {
    expect(calculator.add('4,5,6,7')).toBe(13);
  });

  it("Allow the Add method to handle new lines between numbers (instead of commas)", function () {
    expect(calculator.add('1\n2,3')).toBe(5);
  });

  it("Support different delimiters", function () {
    expect(calculator.add("//;\n1;2")).toBe(3);
  });

  it("Calling Add with a negative number will throw an exception", function () {
    expect(calculator.add('-1,1') instanceof Error).toBe(true);
  });

  it("Numbers bigger than 1000 should be ignored", function () {
    expect(calculator.add("2,1001")).toBe(2);
  });

  it("Delimiters can be of any length", function () {
    expect(calculator.add("//—]\\n1—2—3")).toBe(6);
  });

  it("Allow multiple delimiters", function () {
    expect(calculator.add("//-%\\n1-2%3")).toBe(6);
  });

  it("can also handle multiple delimiters with length longer than one char", function () {
    expect(calculator.add("//-%;\\n1-;2%;3")).toBe(6);
  });

});


