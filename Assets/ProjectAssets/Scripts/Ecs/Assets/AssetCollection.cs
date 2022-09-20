using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Assets
{

    [CreateAssetMenu(menuName = "AssetCollection/new asset collection")]
    public class AssetCollection : ScriptableObject
    {
        [SerializeField]
        private List<GameObject> m_AssetList;

        public List<GameObject> AssetList => m_AssetList;
    }
}
