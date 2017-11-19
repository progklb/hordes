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
	}
}
