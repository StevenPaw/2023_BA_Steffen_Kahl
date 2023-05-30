using System;
using NLG;
using UnityEngine;

namespace Game2
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Game2Manager game2Manager;
        [SerializeField] private float escapeSpeed;
        [SerializeField] private AnimationCurve escapeSpeedCurve;
        [SerializeField] private float escapeProgress;
        [SerializeField] private float escapeTime;
        [SerializeField] private Animator ghostAnimator;
        [SerializeField] private bool captured;
        [SerializeField] private float maxSize;
        [SerializeField] private float minSize;

        public Game2Manager Game2Manager
        {
            get => game2Manager;
            set => game2Manager = value;
        }

        private void Start()
        {
            escapeProgress = 0;
            ghostAnimator.speed = 0;
            game2Manager = FindObjectOfType<Game2Manager>();
            if(ghostAnimator != null) {
                escapeTime = ghostAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / escapeSpeedCurve.Evaluate(game2Manager.Score);
            }
            transform.localScale = Vector2.one * UnityEngine.Random.Range(minSize, maxSize);
        }

        private void Update()
        {
            if (game2Manager.GameState == GameStates.INGAME)
            {
                if (!captured) {
                    ghostAnimator.speed = escapeSpeedCurve.Evaluate(game2Manager.Score);
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
                        game2Manager.TakeDamage();
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
            game2Manager.ReceivePoint();
            escapeProgress = 0;
            ghostAnimator.speed = 1;
            captured = true;
        }
    }
}