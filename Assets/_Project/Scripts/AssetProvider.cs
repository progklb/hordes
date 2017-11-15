using UnityEngine;

using Utilities;

namespace Hordes
{
    public class AssetProvider : MonoBehaviour
    {
        #region PROPERTIES
        public static AssetProvider instance { get; private set; }

        // Character materials
        public Material playerMaterial { get { return m_PlayerMaterial; } }
        public Material innocentMaterial { get { return m_InnocentMaterial; } }
        public Material heroMaterial { get { return m_HeroMaterial; } }
        public Material slaveMaterial { get { return m_SlaveMaterial; } }

		// Prefabs
		public GameObject innocentPrefab { get { return m_InnocentPrefab; } }
		#endregion


		#region VARIABLES
		[SerializeField] private Material m_PlayerMaterial;
        [SerializeField] private Material m_InnocentMaterial;
        [SerializeField] private Material m_HeroMaterial;
        [SerializeField] private Material m_SlaveMaterial;
		[Space(10)]

		// Prefabs
		[SerializeField] private GameObject m_InnocentPrefab;
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