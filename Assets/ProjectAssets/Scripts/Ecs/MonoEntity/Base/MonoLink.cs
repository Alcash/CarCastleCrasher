using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using System;
using System.Linq;
using System.Reflection;

namespace GameCore.MonoEntities.Base
{
	public class MonoLink : MonoEntityBase
	{
		private EcsEntity _entity;

		private Dictionary<Type, MonoEntityBase> _monoEntities;

		public Dictionary<Type, MonoEntityBase> MonoEntities
		{
			get
			{
				Init();
				return _monoEntities;
			}
		}
		private void Awake()
		{
			Init();
		}

		private void Init()
		{
			if (_monoEntities == null)
			{
				_monoEntities = new Dictionary<Type, MonoEntityBase>();
				_monoEntities = GetComponents<MonoEntityBase>()
					.Where(x => x != this)
					.ToDictionary(x => x.GetType(), x => x);
			}
		}
		
		public void Add<T>(T value) where T : MonoEntityBase
		{
			if (_monoEntities.ContainsKey(typeof(T)))
			{
				return;
			}

			_monoEntities.Add(typeof(T), value);
		}

		public void Remove<T>() where T : MonoEntityBase
		{
			if (_monoEntities.ContainsKey(typeof(T)))
			{
				return;
			}

			_monoEntities.Remove(typeof(T));
		}

		public MonoEntity<T> Get<T>() where T : struct
		{
			if (_monoEntities.ContainsKey(typeof(T)))
			{
				if (_monoEntities[typeof(T)] is MonoEntity<T> monoEntity)
				{
					return monoEntity;
				}
			}

			return null;
		}
	
		public override void Make(ref EcsEntity entity)
		{
			_entity = entity;

			foreach (var monoLink in _monoEntities)
			{
				monoLink.Value.Make(ref entity);
			}
		}
	}
}
