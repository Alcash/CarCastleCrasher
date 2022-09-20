
    using System;
    using GameCore.MonoEntities.Base;
    using UnityEngine;

    namespace GameCore.MonoEntities.Components
    {
        [RequireComponent(typeof(Rigidbody))]
        public class RBMonoLink : MonoEntity<RigidbodyEntity>
        {
#if UNITY_EDITOR
            private void OnValidate()
            {
                if (Value.Value == null)
                {
                    Value = new RigidbodyEntity
                    {
                        Value = GetComponent<Rigidbody>()
                    };
                }
            }
#endif
        }
    } 
