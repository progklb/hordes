﻿using UnityEngine;

namespace Hordes
{
	public class AmmoSpawnManager : MonoBehaviour
	{
		#region PROPERTIES
		public static AmmoSpawnManager instance { get; private set; }
		#endregion


		#region VARIABLES
		public bool m_IsSpawning = true;
		public float m_SpawnTimeout = 10;

		private AmmoSpawner[] m_SpawnPoints;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance != null)
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(AmmoSpawnManager).Name);
				return;
			}

			instance = this;

			m_SpawnPoints = FindObjectsOfType<AmmoSpawner>();
		}
		#endregion
	}
}