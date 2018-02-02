using UnityEngine;

using Utilities;

namespace Hordes
{
    public class AssetProvider : MonoBehaviour
    {
        #region PROPERTIES
        public static AssetProvider instance { get; private set; }

		public GameObject innocentPrefab { get { return m_InnocentPrefab; } }
		#endregion


		#region VARIABLES
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