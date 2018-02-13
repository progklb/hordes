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
		// TODO WaveManager should handle enemy spawning

		IEnumerator Start()
		{
			canSpawn = true;

			for (;;)
			{
				yield return new WaitForSeconds(10);
				Spawn();
			}
		}

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


		#region HELPER FUNCTIONS
		void Spawn()
		{
			Instantiate(AssetProvider.instance.stdEnemyPrefab, m_SpawnLocation.position, m_SpawnLocation.rotation);
		}

		bool ShouldToggleCanSpawn(Collider col)
		{
			// Check if the collision is caused by the player or the player's ammo.
			return col.GetComponentInChildren<Ammunition>() != null || col.GetComponentInChildren<Player>() != null;
		}
		#endregion
	}
}