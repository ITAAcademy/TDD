class Password{
  constructor(password){
    this.password = password;
  }

  isValid() {
    if(this.password.length < 5 || this.password.length > 10)
      return false;
    let passwordArray = this.password.split('');
    let re = new RegExp('A-Z');
    return true;
  }
}

module.exports = Password;
