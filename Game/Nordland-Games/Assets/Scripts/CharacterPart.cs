using System.Collections.Generic;
using UnityEngine;

namespace NLG
{
    [System.Serializable]
    public class CharacterPart
    {
        [SerializeField] private Sprite image;
        [SerializeField] private string title;
        [SerializeField] private int requiredXP;
        [SerializeField] private int partID;
        [SerializeField] private CharacterPartTypes type;

        public Sprite Image
        {
            get => image;
            set => image = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public int RequiredXp
        {
            get => requiredXP;
            set => requiredXP = value;
        }

        public int PartID
        {
            get => partID;
            set => partID = value;
        }

        public CharacterPartTypes Type
        {
            get => type;
            set => type = value;
        }
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
        BACKDECO,
    }
}