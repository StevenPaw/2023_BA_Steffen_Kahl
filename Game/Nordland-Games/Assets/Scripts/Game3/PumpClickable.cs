using UnityEngine;
using UnityEngine.EventSystems;

namespace NLG.Game3
{
    public class PumpClickable : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Vector2 forceOnClick;
        private Game3Manager gameManager;
        private Rigidbody2D rb;

        private void Start()
        {
            gameManager = FindObjectOfType<Game3Manager>();
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(gameManager.GameState != GameStates.INGAME) return;
            
            rb.AddForce(forceOnClick, ForceMode2D.Impulse);
            Debug.Log("Clicked");
        }
    }
}