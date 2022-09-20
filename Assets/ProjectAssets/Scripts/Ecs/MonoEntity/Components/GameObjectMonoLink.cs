
  
    using GameCore.MonoEntities.Base;
    using Leopotam.Ecs;

    namespace GameCore.MonoEntities.Components
    {
        public class GameObjectMonoLink : MonoEntity<GameObjectEntity>
        {
            public override void Make(ref EcsEntity entity)
            {
                entity.Get<GameObjectEntity>() = new GameObjectEntity
                {
                    Value = gameObject
                };
            }
        }
    }
