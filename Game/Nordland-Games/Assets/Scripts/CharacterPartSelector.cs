using UnityEngine;
using UnityEngine.UI;

namespace NLG
{
    /// <summary>
    /// A part in the character creator. Used on every button of the creator to make selecting parts possible.
    /// The parts get imported at each runtime from the Webmanager.
    /// </summary>
    public class CharacterPartSelector : MonoBehaviour
    {
        [SerializeField] private Sprite image;
        [SerializeField] private CharacterPartTypes type;
        [SerializeField] private int partID;
        [SerializeField] private bool isSelected;
        [SerializeField] private Image spriteRenderer;
        [SerializeField] private GameObject selectedIndicator;

        public Sprite Image
        {
            get => image;
            set => image = value;
        }

        public CharacterPartTypes Type
        {
            get => type;
            set => type = value;
        }

        public int PartID
        {
            get => partID;
            set => partID = value;
        }

        private CharacterRenderer characterRenderer;

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                characterRenderer = FindObjectOfType<CharacterRenderer>();
            }
            catch
            {
                Debug.LogError("CharacterRenderer not found!");
            }

            spriteRenderer.sprite = image;
        }

        // Update is called once per frame
        void Update()
        {
            if (type == CharacterPartTypes.SKINCOLOR)
            {
                isSelected = partID == characterRenderer.SelectedSkinColor;
            }

            if (type == CharacterPartTypes.EYES)
            {
                isSelected = partID == characterRenderer.SelectedEyes;
            }

            if (type == CharacterPartTypes.MOUTH)
            {
                isSelected = partID == characterRenderer.SelectedMouth;
            }

            if (type == CharacterPartTypes.HAIR)
            {
                isSelected = partID == characterRenderer.SelectedHair;
            }

            if (type == CharacterPartTypes.BOTTOM)
            {
                isSelected = partID == characterRenderer.SelectedBottom;
            }

            if (type == CharacterPartTypes.TOP)
            {
                isSelected = partID == characterRenderer.SelectedTop;
            }

            if (type == CharacterPartTypes.HAT)
            {
                isSelected = partID == characterRenderer.SelectedHat;
            }

            selectedIndicator.SetActive(isSelected);
        }

        public void SelectPart()
        {
            characterRenderer.ChangeSelectedCharacterPart(type, partID);
        }
    }
}