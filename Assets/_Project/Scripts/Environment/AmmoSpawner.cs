using System.Collections;

using UnityEngine;

namespace Hordes
{
	public class AmmoSpawner : MonoBehaviour
	{
		#region VARIABLES
		[SerializeField]
		private bool m_SpawnAtStart;

		[SerializeField]
		private Transform m_SpawnLocation;

		private Ammunition m_CurrentAmmo;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			if (m_SpawnAtStart)
			{
				Spawn();
			}
			else
			{
				StartCoroutine(StartSpawnTimeout());
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator StartSpawnTimeout()
		{
			yield return new WaitForSeconds(AmmoSpawnManager.instance.m_SpawnTimeout);
			Spawn();
		}

		void Spawn()
		{
			var ammoObj = Instantiate(AssetProvider.instance.stdAmmoPrefab, m_SpawnLocation.position, m_SpawnLocation.rotation);

			m_CurrentAmmo = ammoObj.GetComponentInChildren<Ammunition>();
			if (m_CurrentAmmo != null)
			{
				m_CurrentAmmo.onCollected += OnCollection;
			}
		}

		void OnCollection()
		{
			m_CurrentAmmo.onCollected -= OnCollection;
			StartCoroutine(StartSpawnTimeout());
		}
		#endregion
	}
}
