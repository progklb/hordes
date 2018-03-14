using UnityEngine;

using System.Collections;

using Kino;

namespace Hordes
{
	public sealed class EffectsManager : MonoBehaviour
	{
		#region PROPERTIES
		public static EffectsManager instance { get; private set; }

		private AnalogGlitch glitch;
		#endregion


		#region VARIABLES
		[SerializeField] [Range(0f, 1f)] private float m_ScanLineJitterAmount = 0.2f;
		[SerializeField] [Range(0f, 1f)] private float m_VerticalJumpAmount = 0.02f;
		[SerializeField] [Range(0f, 1f)] private float m_ColorDriftAmount = 0.05f;
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance != null)
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(EffectsManager).Name);
				return;
			}

			instance = this;

			glitch = Camera.main.GetComponent<AnalogGlitch>();
		}
		#endregion


		#region PUBLIC API
		public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation = default(Quaternion))
		{
			var effect = Instantiate(prefab, transform);
			effect.transform.position = position;

			if (rotation != default(Quaternion))
			{
				effect.transform.rotation = rotation;
			}

			return effect;
		}

		public void PlayPlayerSpawnEffect()
		{
		}

		public void PlayPlayerDeathEffect()
		{
		}

		public void PlayEnemyDeathEffect()
		{
		}

		public void SetAnalogGlitch(bool glitchEnabled)
		{
			if (glitchEnabled)
			{
				SetGlitchAmounts(m_ScanLineJitterAmount, m_VerticalJumpAmount, m_ColorDriftAmount);
			}
			else
			{
				SetGlitchAmounts(0f, 0f, 0f);
			}
		}

		public void PlayGlitchIntro()
		{
			StartCoroutine(PlayGlitchIntroAsync(1f));
		}
		#endregion


		#region HELPER FUNCTIONS
		IEnumerator PlayGlitchIntroAsync(float duration)
		{
			var fps = 30f;
			var amount = 0.6f;
			var step = amount / (fps * duration);

			while (amount > Mathf.Epsilon)
			{
				SetGlitchAmounts(amount, amount, amount);
				yield return new WaitForSeconds(1f / fps);
				amount -= step;
			}

			SetGlitchAmounts(0f, 0f, 0f);
		}

		void SetGlitchAmounts(float scanLineJitter, float verticalJump, float colorDrift)
		{
			glitch.scanLineJitter = scanLineJitter;
			glitch.verticalJump = verticalJump;
			glitch.colorDrift = colorDrift;
		}
		#endregion
	}
}
