let Password = require('../Password.js');

describe("Is it password correct?", function() {

  it("Is not empty", function() {
    let password = new Password('12345');
    expect(!!password.password.length).toBe(true);
  });

  it("Is valid", function() {
    let password = new Password('');
    expect(password.isValid()).toBe(false);
  });


  it("Is valid", function() {
    let password = new Password('1aA#a');
    expect(password.isValid()).toBe(true);
  });

  it("Error", function() {
    let password = new Password('1aA#a');
    expect(password.trowEr() instanceof Error).toBe(true);
  })

});