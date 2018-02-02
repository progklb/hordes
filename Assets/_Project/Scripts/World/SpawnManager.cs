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
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(SpawnManager).Name);
			}
		}
		#endregion
	}
}