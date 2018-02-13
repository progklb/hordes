using UnityEngine;

using Utilities;

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

		public GameObject stdEnemyPrefab { get { return m_StdEnemyPrefab; } }
		public GameObject stackedEnemyPrefab { get { return m_StackedEnemyPrefab; } }
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
		[SerializeField] private GameObject m_StdEnemyPrefab;
		[SerializeField] private GameObject m_StackedEnemyPrefab;
		[SerializeField] private GameObject m_EnemySpawnedEffectPrefab;
		[SerializeField] private GameObject m_EnemyDestroyedEffectPrefab;
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
        }

        void OnDestroy()
        {
            instance = null;
        }
        #endregion
    }
}