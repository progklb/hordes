using System.Collections;
using UnityEngine;

namespace Hordes
{
	public class EnemySpawner : MonoBehaviour
	{
		#region PROPERTIES
		public bool canSpawn { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField]
		private Transform m_SpawnLocation;
		#endregion


		#region UNITY EVENTS
		void OnTriggerEnter(Collider col)
		{
			if (ShouldToggleCanSpawn(col))
			{
				canSpawn = false;
			}
		}

		void OnTriggerExit(Collider col)
		{
			if (ShouldToggleCanSpawn(col))
			{
				canSpawn = true;
			}
		}
		#endregion


		#region PUBLIC API
		public void Spawn(GameObject enemyPrefab = null)
		{
			Instantiate(enemyPrefab, m_SpawnLocation.position, m_SpawnLocation.rotation);
		}

		public IEnumerator StartAutoSpawnAsync(float spawnInterval)
		{
			canSpawn = true;

			for (;;)
			{
				yield return new WaitForSeconds(spawnInterval);
				Spawn();
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		bool ShouldToggleCanSpawn(Collider col)
		{
			// Check if the collision is caused by the player or the player's ammo.
			return col.GetComponentInChildren<Ammunition>() != null || col.GetComponentInChildren<Player>() != null;
		}
		#endregion
	}
}