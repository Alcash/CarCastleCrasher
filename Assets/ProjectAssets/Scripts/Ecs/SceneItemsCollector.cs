using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.MonoEntities.Base
{
    public class SceneItemsCollector : MonoBehaviour
    {
        public MonoEntityBase[] GetCollectItems()
        {
            return FindObjectsOfType<MonoEntityBase>();
           //return FindObjectsOfType<MonoEntityBase>().GroupBy(x=> x.gameObject).Select(x=>x.FirstOrDefault()).ToArray();
        }
    }
}
