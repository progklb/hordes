using UnityEngine;
using UnityEngine.UI;

namespace Hordes
{
	public class DebugManager : MonoBehaviour
	{
		#region VARIABLES
		[Space(10)]
		public Button m_SpawnToggleButton;

		[Space(10)]
		public Text m_TimeText;
		public Text m_ScoreText;
		public Text m_KillsText;
		public Text m_ShotsText;

		private bool m_IsDisplaying;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			SetButtonText();
			SetUIVisibility(false);
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				ToggleDebugDisplay();
			}

			if (m_IsDisplaying)
			{
				UpdateStatsText();
			}
		}
		#endregion

		#region PUBLIC API
		public void ToggleDebugDisplay()
		{
			SetUIVisibility(!m_IsDisplaying);
		}

		public void ToggleEnemySpawning()
		{
			AmmoSpawnManager.instance.m_IsSpawning = !AmmoSpawnManager.instance.m_IsSpawning;
			m_SpawnToggleButton.GetComponentInChildren<Text>().text = string.Format("Spawning: {0}", AmmoSpawnManager.instance.m_IsSpawning);
		}

		public void SpawnPlayer()
		{
			if (!Player.isAlive)
			{
				Instantiate(AssetProvider.instance.playerPrefab);
			}
		}
		#endregion


		#region HELPER FUNCTIONS
		void SetUIVisibility(bool isDisplaying)
		{
			m_IsDisplaying = isDisplaying;

			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(isDisplaying);
			}
		}

		void SetButtonText()
		{
			var spawnBtnTxt = m_SpawnToggleButton.GetComponentInChildren<Text>();
			if (spawnBtnTxt != null)
			{
				spawnBtnTxt.text = string.Format("Spawning: {0}", AmmoSpawnManager.instance.m_IsSpawning);
			}
		}

		void UpdateStatsText()
		{
			m_TimeText.text = string.Format("Time: {0:F2}", ScoreManager.instance.time.ToString());

			m_ScoreText.text = ScoreManager.instance.score.ToString();
			m_KillsText.text = ScoreManager.instance.kills.ToString();
			m_ShotsText.text = ScoreManager.instance.shots.ToString();
		}
		#endregion
	}
}
