using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public struct SpawnData
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Parent;
    }
}
