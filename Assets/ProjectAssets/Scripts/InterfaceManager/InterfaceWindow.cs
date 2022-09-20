using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCore.InterfaceManger
{
    public class InterfaceWindow : MonoBehaviour
    {
        [SerializeField] private string m_InterfaceKey;
        public string InterfaceKey => m_InterfaceKey;

        [SerializeField, Tooltip("Optional")] private Button m_CloseButton;

        private void Awake()
        {
            if (m_CloseButton)
                m_CloseButton.onClick.AddListener(CloseButtonHandler);
        }

        private void CloseButtonHandler()
        {
            InterfaceManager.CloseLastView();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }


        public void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
