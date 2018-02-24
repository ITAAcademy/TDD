class Password {

  constructor(password) {
    this.password = password;
  }

  isValid() {
    if (this.password.length == 0) this.empty = true; else this.empty = false;
    if (this.password.length < 5) this.lessFive = true; else this.lessFive = false;
    if (this.password.length > 10) this.moreTen = true; else this.moreTen = false;
    if (this.password.search(' ') != -1) this.space = true; else this.space = false;
    if (this.password.search(/[^\wа-яА-Яії]/) != -1) this.banSymbol = true; else this.banSymbol = false;
  }

}

module.exports = Password;
