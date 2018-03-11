using UnityEngine;

using System;

namespace Hordes
{
	public class ScoreManager : MonoBehaviour
	{
		#region EVENTS
		public static event Action onScoreReset = delegate { };
		public static event Action onScoreUpdate = delegate { };
		#endregion


		#region PROPERTIES
		public static ScoreManager instance { get; private set; }

		public float time { get { return m_Time; } }
		public int score { get { return m_Score; } }
		public int kills { get { return m_Kills; } }
		public int shots { get { return m_Shots; } }
		#endregion


		#region VARIABLES
		public float m_Time;
		public int m_Score;
		public int m_Kills;
		public int m_Shots;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance == null)
			{
				instance = this;

				Player.onAttack += OnPlayerAttack;
				Enemy.onDestroyed += OnEnemyKilled;
			}
			else
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(ScoreManager).Name);
			}
		}

		void Update()
		{
			if (Player.isAlive)
			{
				m_Time += Time.deltaTime;
			}
		}

		void OnDestroy()
		{
			Player.onAttack -= OnPlayerAttack;
			Enemy.onDestroyed -= OnEnemyKilled;
		}
		#endregion


		#region PUBLIC API
		public void ResetScore()
		{
			m_Time = m_Score = m_Shots = m_Kills = 0;
			onScoreReset();
		}
		#endregion


		#region EVENT HANDLERS
		void OnPlayerAttack()
		{
			m_Shots++;
			onScoreUpdate();
		}

		void OnEnemyKilled(Enemy enemy)
		{
			m_Kills++;
			m_Score += enemy.scoreValue;
		}
		#endregion
	}
}
