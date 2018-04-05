using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using EnemyClass = Hordes.Enemy.EnemyClass;

namespace Hordes
{
	public sealed class WaveManager : MonoBehaviour
	{
		#region EVENTS
		public static event Action<int> onWaveStarted = delegate { };
		public static event Action<int> onWaveEnded = delegate { };
		#endregion


		#region PROPERTIES
		public static WaveManager instance { get; private set; }

		public int waveNumber { get; private set; }
		public bool waveComplete { get; private set; }
		public bool isSpawning { get; private set; }

		private float spawnInterval { get; set; }

		private float spawnChanceClass1 { get; set; }
		private float spawnChanceClass2 { get; set; }
		private float spawnChanceClass3 { get; set; }

		private List<Enemy> m_Enemies = new List<Enemy>();
		#endregion


		#region VARIABLES
		[SerializeField] private float m_SpawnInterval = 10f;

		private EnemySpawner[] m_SpawnPoints;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			if (instance != null)
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(WaveManager).Name);
				return;
			}

			instance = this;

			m_SpawnPoints = FindObjectsOfType<EnemySpawner>();

			Enemy.onSpawned += OnEnemySpawned;
			Enemy.onDestroyed += OnEnemyDestroyed;

			Player.onPlayerSpawn += StartWaves;
			Player.onPlayerDeath += EndWaves;
		}
		#endregion


		#region PUBLIC API
		public void ResetAll()
		{
			waveNumber = 1;
			spawnInterval = m_SpawnInterval;

			spawnChanceClass1 = 1f;
			spawnChanceClass2 = 0f;
			spawnChanceClass3 = 0f;

			m_Enemies.Clear();
		}

		public void StartWaves()
		{
			ResetAll();
			StartCoroutine(StartWaveAsync());

			onWaveEnded += OnWaveEnded;
		}

		public void EndWaves()
		{
			StopAllCoroutines();

			foreach (var enemy in m_Enemies)
			{
				enemy.CleanUp();
			}

			onWaveEnded -= OnWaveEnded;
		}
		#endregion


		#region HELPER FUNCTIONS - WAVE MANAGEMENT
		IEnumerator StartWaveAsync()
		{
			onWaveStarted(waveNumber);
			Debug.LogFormat("Starting wave {0}.", waveNumber);

			var waveStartTime = Time.time;
			isSpawning = true;

			while (isSpawning)
			{
				yield return new WaitForSeconds(spawnInterval);

				// Select random spawn point and spawn an enemy
				var randomPoint = UnityEngine.Random.Range(0, m_SpawnPoints.Length - 1);
				m_SpawnPoints[randomPoint].Spawn(GetRandomEnemy());

				isSpawning = Time.time - waveStartTime < 60;

				Debug.LogFormat("Spawned enemy.");
			}

			Debug.LogFormat("Final enemy spawned. Waiting for player to kill enemy...");
			yield return new WaitUntil(() => m_Enemies.Count == 0);
			onWaveEnded(waveNumber);
		}

		void OnWaveEnded(int waveNumber)
		{
			NextWave();
			StartCoroutine(StartWaveAsync());
		}

		void NextWave()
		{
			waveNumber++;

			// Decrease spawn interval
			spawnInterval -= 0.2f;
			spawnInterval = Mathf.Clamp(spawnInterval, 5f, 10f);

			switch (waveNumber)
			{
				case 4:
					spawnChanceClass1 = 0.8f;
					spawnChanceClass2 = 0.2f;
					break;
				case 5:
					spawnChanceClass1 = 0.6f;
					spawnChanceClass2 = 0.4f;
					break;
				case 6:
					spawnChanceClass1 = 0.4f;
					spawnChanceClass2 = 0.6f;
					break;
				case 7:
					spawnChanceClass1 = 0.2f;
					spawnChanceClass2 = 0.6f;
					spawnChanceClass3 = 0.2f;
					break;
				case 8:
					spawnChanceClass1 = 0.2f;
					spawnChanceClass2 = 0.4f;
					spawnChanceClass3 = 0.4f;
					break;
				case 9:
					spawnChanceClass1 = 0.2f;
					spawnChanceClass2 = 0.2f;
					spawnChanceClass3 = 0.6f;
					break;
			}

			Debug.LogFormat("Wave: {0} -- SpawnInterval: {1} -- Chance: 1={2}  2={3}  3={4}", waveNumber, spawnInterval, spawnChanceClass1, spawnChanceClass2, spawnChanceClass3);
		}
		#endregion


		#region HELPER FUNCTIONS - ENEMIES
		GameObject GetRandomEnemy()
		{
			EnemyClass enemyClass;

			var rand = UnityEngine.Random.value;
			if (rand <= spawnChanceClass1)
			{
				enemyClass = EnemyClass.Class1;
			}
			else if (rand <= (spawnChanceClass1 + spawnChanceClass2))
			{
				enemyClass = EnemyClass.Class2;
			}
			else
			{
				enemyClass = EnemyClass.Class3;
			}

			return AssetProvider.instance.GetEnemyPrefab(enemyClass);
		}

		void OnEnemySpawned(Enemy enemy)
		{
			m_Enemies.Add(enemy);
		}

		void OnEnemyDestroyed(Enemy enemy)
		{
			if (m_Enemies.Contains(enemy))
			{
				m_Enemies.Remove(enemy);
			}
			else
			{
				Debug.LogErrorFormat("[{0}] An enemy was destroyed but this manager was not keeping track of it.", typeof(WaveManager).Name);
			}
		}
		#endregion
	}
}
