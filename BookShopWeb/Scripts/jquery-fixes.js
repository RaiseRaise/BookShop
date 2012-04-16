$.validator.methods.number = function (value, element) {
    return this.optional(element) || /^(?:\d+|\d{1,3}(?:,\d{2})+)(?:\.\d+)?$/.test(value);
}