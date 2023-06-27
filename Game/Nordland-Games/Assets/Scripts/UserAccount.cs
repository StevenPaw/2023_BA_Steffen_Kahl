using System.Collections.Generic;
using UnityEngine;

namespace NLG
{
    /// <summary>
    /// A scriptable object to store all data about a user account.
    /// </summary>
    public class UserAccount : ScriptableObject
    {
        public string username;
        public int xp;
        public List<CharacterPart> aquiredCharacterParts;

        public UserAccount(string username, int xp, List<CharacterPart> aquiredCharacterParts = null)
        {
            this.username = username;
            this.xp = xp;
            this.aquiredCharacterParts = aquiredCharacterParts;
        }
    }
}