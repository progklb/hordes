using System;

using UnityEngine;
using UnityEngine.AI;

namespace Hordes
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : MonoBehaviour
	{
		#region TYPES
		/// <summary>
		/// Indicates the strength of enemy, where the higher the class the more powerful.
		/// </summary>
		public enum EnemyClass
		{
			Class1 = 1,
			Class2 = 2,
			Class3 = 3
		}
		#endregion


		#region EVENTS
		public static Action<Enemy> onSpawned = delegate { };
		public static Action<Enemy> onDestroyed = delegate { };
		#endregion


		#region PROPERTIES
		public int health { get { return m_Health; } }
		public int scoreValue { get { return m_ScoreValue; } }
		public EnemyClass classLevel { get { return m_ClassLevel; } }
		#endregion


		#region VARIABLES
		[Header("Optional Overrides")]
		[SerializeField] protected GameObject m_SpawnedEffect;
		[SerializeField] protected GameObject m_DestroyedEffect;
		[SerializeField] protected GameObject m_CleanupEffect;

		[Space(10)]
		[SerializeField] protected int m_Health = 1;
		[SerializeField] protected int m_ScoreValue;
		[SerializeField] protected EnemyClass m_ClassLevel;

		protected NavMeshAgent m_Agent;
		#endregion


		#region UNITY EVENTS
		protected virtual void Start()
		{
			m_Agent = GetComponent<NavMeshAgent>();

			// Apply default values if there are no overrides
			if (m_SpawnedEffect == null)	m_SpawnedEffect = AssetProvider.instance.enemySpawnedEffectPrefab;
			if (m_DestroyedEffect == null)	m_DestroyedEffect = AssetProvider.instance.enemyDestroyedEffectPrefab;
			if (m_CleanupEffect == null)	m_CleanupEffect = AssetProvider.instance.enemyCleanedUpEffectPrefab;

			EffectsManager.instance.Instantiate(m_SpawnedEffect, transform.position);

			onSpawned(this);
		}

		protected abstract void Update();

		private void OnTriggerEnter(Collider col)
		{
			var player = col.GetComponentInChildren<Player>();
			if (player != null)
			{
				player.TakeDamage();
				TakeDamage(1, transform.forward);
			}
		}
		#endregion


		#region PUBLIC API
		public virtual bool TakeDamage(int damage, Vector3 damageDir = default(Vector3))
        {
            m_Health -= damage;

            if (m_Health <= 0)
            {
				EffectsManager.instance.PlayEnemyDeathEffect();
				SpawnHitEffect(damageDir);
				DestroySelf();
            }

            return false;
        }

		public virtual void CleanUp()
		{
			EffectsManager.instance.Instantiate(m_CleanupEffect, transform.position);

			Destroy(gameObject);
		}
		#endregion


		#region HELPER FUNCTIONS
		protected virtual void DestroySelf()
		{
			onDestroyed(this);

			Destroy(gameObject);
		}

		protected virtual void SpawnHitEffect(Vector3 damageDir = default(Vector3))
		{
			if (m_DestroyedEffect != null && damageDir != default(Vector3))
			{
				var effect = EffectsManager.instance.Instantiate(m_DestroyedEffect, transform.position);
				effect.transform.forward = damageDir;
			}
		}
		#endregion
	}
}
