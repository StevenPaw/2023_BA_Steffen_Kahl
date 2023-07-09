using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace NLG.Game2
{
    /// <summary>
    /// A script that holds all the needed data and behaviour for a ghost in Game2.
    /// </summary>
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private IGameManager gameManager;
        [SerializeField] private float escapeSpeed;
        [SerializeField] private AnimationCurve escapeSpeedCurve;
        [SerializeField] private AnimationCurve lightIntensityCurve;
        [SerializeField] private Light2D light;
        [SerializeField] private float escapeProgress;
        [SerializeField] private float escapeTime;
        [SerializeField] private Animator ghostAnimator;
        [SerializeField] private bool captured;
        [SerializeField] private float maxSize;
        [SerializeField] private float minSize;

        public IGameManager GameManager
        {
            get => gameManager;
            set => gameManager = value;
        }

        private void Start()
        {
            escapeProgress = 0;
            ghostAnimator.speed = 0;
            gameManager = FindObjectsOfType<MonoBehaviour>().OfType<IGameManager>().FirstOrDefault();
            if(ghostAnimator != null) {
                escapeTime = ghostAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / escapeSpeedCurve.Evaluate(gameManager.GetScore());
            }
            transform.localScale = Vector2.one * UnityEngine.Random.Range(minSize, maxSize);
        }

        private void Update()
        {
            if (gameManager.GetState() == GameStates.INGAME)
            {
                if (!captured) {
                    ghostAnimator.speed = escapeSpeedCurve.Evaluate(gameManager.GetScore());
                }
                else
                {
                    escapeTime = ghostAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                    ghostAnimator.speed = 1;
                }

                escapeProgress += Time.deltaTime;
                if (escapeProgress >= escapeTime)
                {
                    if (!captured)
                    {
                        gameManager.TakeDamage();
                    }

                    Destroy(gameObject);
                }
            }
            else
            {
                ghostAnimator.speed = 0;
            }
        }

        private void OnMouseDown()
        {
            ghostAnimator.SetTrigger("Despawn");
            gameManager.ReceivePoint();
            escapeProgress = 0;
            ghostAnimator.speed = 1;
            captured = true;
            DOTween.To(()=> light.intensity, x=> light.intensity = x, 0, 0.1f);
        }
    }
}