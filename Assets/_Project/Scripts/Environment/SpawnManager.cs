using UnityEngine;

namespace Hordes
{
	public class SpawnManager : MonoBehaviour
	{
		#region PROPERTIES
		public static SpawnManager instance { get; private set; }
		#endregion


		#region VARIABLES
		public bool m_IsSpawning = true;

		private AmmoSpawner[] m_SpawnPoints;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance != null)
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(SpawnManager).Name);
				return;
			}

			instance = this;

			m_SpawnPoints = FindObjectsOfType<AmmoSpawner>();
		}
		#endregion
	}
}