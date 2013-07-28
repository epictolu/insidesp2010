//Register namespace
if (typeof (Wingtip) == 'undefined')
    Type.registerNamespace('Wingtip');

//Constructor
Wingtip.SimpleList = function (element) {
    element.listControl = this;
    this.element = element;
}

//Prototype
Wingtip.SimpleList.prototype = {
    element: null,
    items: null,
    add: function (text) {
        this.items += '<li>' + text + '</li>';
        element.innerHTML = '<ol>' + this.items + '</ol>';
    }
}

//Register the class
Wingtip.SimpleList.registerClass('Wingtip.SimpleList');