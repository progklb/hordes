using UnityEngine;
using UnityEngine.PostProcessing;

using System.Collections;

using Kino;

namespace Hordes
{
	public sealed class EffectsManager : MonoBehaviour
	{
		#region PROPERTIES
		public static EffectsManager instance { get; private set; }

		private AnalogGlitch glitch { get; set; }
		private bool isPlayingEffect { get; set; }
		#endregion


		#region VARIABLES
		[SerializeField] [Range(0f, 1f)] private float m_ScanLineJitterAmount = 0.2f;
		[SerializeField] [Range(0f, 1f)] private float m_VerticalJumpAmount = 0.02f;
		[SerializeField] [Range(0f, 1f)] private float m_ColorDriftAmount = 0.05f;

		[SerializeField] [Range(1f, 20f)] private float m_EffectSpeed = 5f;
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
			StartCoroutine(PlayGlitchIntroAsync(1f));
		}

		public void PlayEnemyDeathEffect()
		{
			if (!isPlayingEffect)
			{
				StartCoroutine(PlayEffectAsync());
			}
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
		#endregion


		#region HELPER FUNCTIONS - COROUTINES
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

		IEnumerator PlayEffectAsync()
		{
			isPlayingEffect = true;

			var profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
			var startTime = Time.time;

			var settings = profile.chromaticAberration.settings;

			do
			{
				settings.intensity = Mathf.Sin((Time.time - startTime) * m_EffectSpeed);
				profile.chromaticAberration.settings = settings;
				yield return null;
			}
			while (settings.intensity >= 0);

			isPlayingEffect = false;
		}
		#endregion


		#region HELPER FUNCTIONS - GENERAL
		void SetGlitchAmounts(float scanLineJitter, float verticalJump, float colorDrift)
		{
			glitch.scanLineJitter = scanLineJitter;
			glitch.verticalJump = verticalJump;
			glitch.colorDrift = colorDrift;
		}
		#endregion
	}
}
