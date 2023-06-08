mergeInto(LibraryManager.library, {
    
    GetInputFromVirtualKeyboard: function () {
        let person = prompt("Wähle einen Benutzernamen", "");
        myGameInstance.SendMessage("WebManager", "GenerateUserByJS", person);
    },

});