using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace NLG
{
    /// <summary>
    /// A fix for WebGL input fields on mobile. Not supported by Unity at the moment.
    /// </summary>
    public class WebGLTextfix : MonoBehaviour
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            [DllImport("__Internal")]
            private static extern string GetInputFromVirtualKeyboard();
        #endif

        private string output;

        [SerializeField] private TMP_InputField inputField;

        public void GetInput()
        {
            #if !UNITY_EDITOR && UNITY_WEBGL
                inputField.text = GetInputFromVirtualKeyboard();
            #endif
        }
    }
}