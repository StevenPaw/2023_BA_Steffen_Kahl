using UnityEngine;

namespace NLG
{
    /// <summary>
    /// An object to hold the data of a character part.
    /// </summary>
    [System.Serializable]
    public class CharacterPart
    {
        [SerializeField] private Sprite image;
        [SerializeField] private Sprite previewImage;
        [SerializeField] private string title;
        [SerializeField] private int requiredXP;
        [SerializeField] private int partID;
        [SerializeField] private CharacterPartTypes type;

        public Sprite Image
        {
            get => image;
            set => image = value;
        }

        public Sprite PreviewImage
        {
            get => previewImage;
            set => previewImage = value;
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