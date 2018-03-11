using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using Utilities;

using EnemyClass = Hordes.Enemy.EnemyClass;

namespace Hordes
{
	/// <summary>
	/// Provides global access to standard assets.
	/// </summary>
    public class AssetProvider : MonoBehaviour
    {
        #region PROPERTIES
        public static AssetProvider instance { get; private set; }

		public GameObject playerPrefab { get { return m_PlayerPrefab; } }

		public GameObject stdAmmoPrefab { get { return m_StdAmmoPrefab; } }
		public GameObject ammoSpawnedEffectPrefab { get { return m_AmmoSpawnedEffectPrefab; } }
		public GameObject ammoCollectedEffectPrefab { get { return m_AmmoCollectedEffectPrefab; } }
		public GameObject ammoDestroyedEffectPrefab { get { return m_AmmoDestroyedEffectPrefab; } }

		public GameObject enemySpawnedEffectPrefab { get { return m_EnemySpawnedEffectPrefab; } }
		public GameObject enemyDestroyedEffectPrefab { get { return m_EnemyDestroyedEffectPrefab; } }
		#endregion


		#region VARIABLES
		[SerializeField] private GameObject m_PlayerPrefab;

		[Space(10)]
		[Header("Ammunition")]
		[SerializeField] private GameObject m_StdAmmoPrefab;
		[SerializeField] private GameObject m_AmmoSpawnedEffectPrefab;
		[SerializeField] private GameObject m_AmmoCollectedEffectPrefab;
		[SerializeField] private GameObject m_AmmoDestroyedEffectPrefab;

		[Space(10)]
		[Header("Enemy")]
		[SerializeField] private GameObject[] m_EnemyPrefabs;
		[SerializeField] private GameObject m_EnemySpawnedEffectPrefab;
		[SerializeField] private GameObject m_EnemyDestroyedEffectPrefab;

		/// A cache for convenient runtime retrieval
		private Dictionary<EnemyClass, List<GameObject>> m_CachedEnemyPrefabs;
		#endregion


		#region UNITY EVENTS
		void Awake()
        {
            if (instance != null)
            {
                LogContext.LogErrorFormat(this, "There is more than one manager instance in the scene.");
                return;
            }

            instance = this;

			CacheEnemyPrefabs();
		}

        void OnDestroy()
        {
            instance = null;
        }
		#endregion


		#region PUBLIC API
		public GameObject GetEnemyPrefab(EnemyClass enemyClass)
		{
			var prefabs = GetEnemyPrefabs(enemyClass);
			if (prefabs.Count > 0)
			{
				var randomIndex = UnityEngine.Random.Range(0, prefabs.Count - 1);
				return prefabs[randomIndex];
			}
			else
			{
				return null;
			}
		}

		public List<GameObject> GetEnemyPrefabs(EnemyClass enemyClass)
		{
			if (m_CachedEnemyPrefabs.ContainsKey(enemyClass))
			{
				return m_CachedEnemyPrefabs[enemyClass];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns the classes of enemies that are available as prefabs.
		/// </summary>
		public List<EnemyClass> GetEnemyClassesAscending()
		{
			var result = m_CachedEnemyPrefabs.Keys.ToList();
			result.Sort();

			foreach (var s in result)
			{
				Debug.Log("------" + s);
			}

			return result;
		}
		#endregion


		#region HELPER FUNCTIONS
		void CacheEnemyPrefabs()
		{
			m_CachedEnemyPrefabs = new Dictionary<EnemyClass, List<GameObject>>();

			foreach (var enemyObj in m_EnemyPrefabs)
			{
				var enemyClass = enemyObj.GetComponentInChildren<Enemy>().classLevel;

				if (!m_CachedEnemyPrefabs.ContainsKey(enemyClass))
				{
					m_CachedEnemyPrefabs.Add(enemyClass, new List<GameObject>());
				}

				m_CachedEnemyPrefabs[enemyClass].Add(enemyObj);
			}
		}
		#endregion
	}
}