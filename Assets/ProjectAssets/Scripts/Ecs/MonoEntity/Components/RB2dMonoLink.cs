
    using System;
    using GameCore.MonoEntities.Base;
    using UnityEngine;

    namespace GameCore.MonoEntities.Components
    {
        [RequireComponent(typeof(Rigidbody2D))]
        public class RB2dMonoLink : MonoEntity<Rigidbody2DEntity>
        {
#if UNITY_EDITOR
            private void OnValidate()
            {
                if (Value.Value == null)
                {
                    Value = new Rigidbody2DEntity
                    {
                        Value = GetComponent<Rigidbody2D>()
                    };
                }
            }
#endif
        }
    } 
