using UnityEngine;

namespace Hordes
{
	public class EffectsManager : MonoBehaviour
	{
		#region PROPERTIES
		public static EffectsManager instance { get; private set; }
		#endregion


		#region UNITY EVENTS
		void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogFormat("[{0}] This is more than one manager instance in the scene.", typeof(EffectsManager).Name);
			}
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
		#endregion
	}
}
