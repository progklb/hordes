using System.Collections;

using UnityEngine;

using Utilities;

namespace Hordes
{
	public class SpawnPoint : MonoBehaviour
	{
		#region TYPES
		public enum SpawnType
		{
			Innocent
		}
		#endregion


		#region VARIABLES
		public SpawnType m_Type;
		public Transform m_SpawnLocation;

		public float m_SpawnDelay = 30f;
		public bool m_SpawnAtStart;
		#endregion


		#region UNITY EVENTS
		IEnumerator Start()
		{
			if (m_SpawnAtStart)
			{
				Spawn();
			}

			for (;;)
			{
				yield return new WaitForSeconds(m_SpawnDelay);
				Spawn();
			}
		}
		#endregion


		#region EVENT HANDLERS
		void OnNotifiedTriggerStay(Collider col)
		{
			if (col.GetComponentInChildren<Character>() != null || col.GetComponentInChildren<PlayerController>() != null)
			{
				// Reset timeout
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void Spawn()
		{
			switch (m_Type)
			{
				case SpawnType.Innocent:
					Instantiate(AssetProvider.instance.innocentPrefab, m_SpawnLocation.position, m_SpawnLocation.rotation);
					break;
			}
		}
		#endregion
	}
}
