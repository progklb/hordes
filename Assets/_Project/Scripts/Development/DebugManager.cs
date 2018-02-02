using UnityEngine;
using UnityEngine.UI;

namespace Hordes
{
	public class DebugManager : MonoBehaviour
	{
		#region VARIABLES
		public Button m_SpawnToggleButton;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			SetButtonText();
		}
		#endregion

		#region PUBLIC API
		public void ToggleSpawning()
		{
			SpawnManager.instance.m_IsSpawning = !SpawnManager.instance.m_IsSpawning;
			m_SpawnToggleButton.GetComponentInChildren<Text>().text = string.Format("Spawning: {0}", SpawnManager.instance.m_IsSpawning);
		}
		#endregion


		#region HELPER FUNCTIONS
		void SetButtonText()
		{
			var spawnBtnTxt = m_SpawnToggleButton.GetComponentInChildren<Text>();
			if (spawnBtnTxt != null)
			{
				spawnBtnTxt.text = string.Format("Spawning: {0}", SpawnManager.instance.m_IsSpawning);
			}
		}
		#endregion
	}
}
