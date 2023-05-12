mergeInto(LibraryManager.library, {

    Hello: function () {
        window.alert("Hello, world!");
    },
    
    GetInputFromVirtualKeyboard: function () {
        let person = prompt("Wähle einen Benutzernamen", "");
        myGameInstance.SendMessage("WebManager", "GenerateUserByJS", person);
    },

});