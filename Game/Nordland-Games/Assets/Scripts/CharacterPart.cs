using UnityEngine;

namespace NLG
{
    public class CharacterPart
    {
        [SerializeField] private Sprite image;
        [SerializeField] private string title;
        [SerializeField] private CharacterPartTypes type;
    }

    public enum CharacterPartTypes
    {
        NONE,
        SKINCOLOR,
        EYES,
        MOUTH,
        HAIR,
        BOTTOM,
        TOP,
        HAT,
    }
}