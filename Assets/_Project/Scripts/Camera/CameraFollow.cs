using UnityEngine;

namespace Hordes
{
	/// <summary>
	/// Follows the player at a specified offet.
	/// </summary>
	public class CameraFollow : MonoBehaviour
	{
		#region PROPERTIES
		public Player player { get { return Player.instance; } }
		#endregion


		#region VARIABLES
		public float m_Smoothing = 100;
		public Vector3 m_InitialOffset;

		private Transform m_Transform;
		/// Reusables var to prevent per-frame memory allocation
		private Vector3 m_Position;
		#endregion


		#region UNITY EVENTS
		private void Awake()
		{
			m_Transform = transform;
		}

		void FixedUpdate()
		{
			if (player == null)
			{
				return;
			}

			m_Position = m_Transform.position - m_InitialOffset;

			m_Position.x = Mathf.Lerp(m_Position.x, player.transform.position.x, m_Smoothing * Time.deltaTime);
			m_Position.y = Mathf.Lerp(m_Position.y, player.transform.position.y, m_Smoothing * Time.deltaTime);
			m_Position.z = Mathf.Lerp(m_Position.z, player.transform.position.z, m_Smoothing * Time.deltaTime);

			m_Transform.position = m_Position + m_InitialOffset;
		}
		#endregion
	}
}