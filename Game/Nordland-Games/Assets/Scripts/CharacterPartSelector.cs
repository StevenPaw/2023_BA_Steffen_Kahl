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
        [SerializeField] private CharacterPartTypes type;
        [SerializeField] private int partID;
        [SerializeField] private bool isSelected;
        [SerializeField] private Image spriteRenderer;
        [SerializeField] private GameObject availabilityIndicator;
        [SerializeField] private GameObject selectedIndicator;
        [SerializeField] private CharacterPart part;
        [SerializeField] private bool isAvailable;
        [SerializeField] private Color colorAvailable;
        [SerializeField] private Color colorNotAvailable;
        [SerializeField] private CharacterEditorManager characterEditorManager;

        public bool IsAvailable => isAvailable;

        public CharacterPart Part
        {
            get => part;
            set => part = value;
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
            
            try
            {
                characterEditorManager = FindObjectOfType<CharacterEditorManager>();
            }
            catch
            {
                Debug.LogError("CharacterEditorManager not found!");
            }
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

        public void ChangePart(CharacterPart part)
        {
            this.part = part;
            Type = part.Type;
            PartID = part.PartID;
            spriteRenderer.sprite = part.Image;
            isAvailable = WebManager.instance.UserXP >= part.RequiredXp; //Check if user has enough XP to use part
            spriteRenderer.color = isAvailable ? colorAvailable : colorNotAvailable; //Change renderer color accordingly
            availabilityIndicator.SetActive(!isAvailable);
        }

        public void SelectPart()
        {
            if (isAvailable)
            {
                characterRenderer.ChangeSelectedCharacterPart(type, partID);
            } else
            {
                characterEditorManager.ShowXPMessage(part);
            }
        }
    }
}