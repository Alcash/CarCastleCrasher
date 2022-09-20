
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectCore.InterfaceManger
{
    public class InterfaceManager : MonoBehaviour
    {
        private static InterfaceManager _instance;
        [SerializeField] private List<InterfaceWindow> m_WindowList = new List<InterfaceWindow>();           

        private Dictionary<string, InterfaceWindow> m_InterfaceViews = new Dictionary<string, InterfaceWindow>();
        private List<string> _windowsList = new List<string>();

        private void Awake()
        {
            _instance = this;           
            foreach (var window in m_WindowList)
            {
                m_InterfaceViews.Add(window.InterfaceKey, window);
            }    
        }   

        private bool OpenInterface(string key, out InterfaceWindow interfaceWindow)
        {
            interfaceWindow = null;
            if (m_InterfaceViews.ContainsKey(key) == false)
                return false;

            foreach (var view in m_InterfaceViews)
            {
                m_InterfaceViews[view.Key].SetActive(key == view.Key);
            }
            _windowsList.Add(key);
            interfaceWindow = m_InterfaceViews[key];
            return true;
        }

        private void CloseLast()
        {
            var lastKey = _windowsList.Last();
            _windowsList.Remove(lastKey);

            foreach (var view in m_InterfaceViews)
            {
                m_InterfaceViews[view.Key].SetActive(_windowsList.Last() == view.Key);
            }
        }       



        public static bool OpenView(string key, out InterfaceWindow interfaceWindow)
        {           
            return _instance.OpenInterface(key,out interfaceWindow);           
        }        

        public static void CloseLastView()
        {
            _instance.CloseLast();
        }
    }
}
