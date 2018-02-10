using System.Collections;

using UnityEngine;

namespace Hordes
{
	public class AmmoSpawner : MonoBehaviour
	{
		#region VARIABLES
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
				if (SpawnManager.instance.m_IsSpawning)
				{
					Spawn();
				}
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void Spawn()
		{
			Instantiate(AssetProvider.instance.stdAmmoPrefab, m_SpawnLocation.position, m_SpawnLocation.rotation);
		}
		#endregion
	}
}
