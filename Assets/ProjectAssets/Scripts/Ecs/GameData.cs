using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  GameCore.Config
{
    [CreateAssetMenu(menuName = "Config/StaticData", fileName = "StaticData", order = 0)]
    public class GameData : ScriptableObject
    {
        public GameObject PlayerPrefab;
    }
}

