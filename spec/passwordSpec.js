let pas = require('./../Password.js');


describe("!-=| Checking PasswordIsValid |=-!", function() {

  	var password = new pas(''); // <- enter the password 
    password.isValid();

  it(' !---= WARNING password has SPACE =---!', function() {

    expect(password.space).toBe(false);
  });

  it(' !---= WARNING passwoord is EMPTY =---!', function() {
    
    expect(password.empty).toBe(false);
  });

  it(' !-= WARNING passwoord size is less than FIVE =-!', function() {

    expect(password.lessFive).toBe(false);
  });

  it(' !-= WARNING passwoord size is more than TEN =-!', function() {

    expect(password.moreTen).toBe(false);
  });

  it(' !---= WARNING passwoord has BAN SYMBOL =---!', function() {

    expect(password.banSymbol).toBe(false);
  });

});