using System.Collections.Generic;
using UnityEngine;

namespace NLG
{
    public class UserAccount : MonoBehaviour
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