using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWaitTimeView : MonoBehaviour
{
    [SerializeField] private Animator viewAnimator;
    
    public void ToggleView()
    {
        viewAnimator.SetBool("isActive", !viewAnimator.GetBool("isActive"));
    }
}
