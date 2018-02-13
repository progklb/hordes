using UnityEngine;

using System.Collections.Generic;

namespace Hordes
{
    public class StackedEnemy : Enemy
    {
		#region VARIABLES
		[SerializeField]
		private GameObject[] m_Components;

		private List<GameObject> m_ComponentsList;
		#endregion


		#region UNITY EVENTS
		protected override void Start()
		{
			base.Start();

			m_ComponentsList = new List<GameObject>(m_Components);
		}

		protected override void Update()
        {
            if (Player.isAlive)
            {
				m_Agent.isStopped = false;
				m_Agent.destination = Player.instance.transform.position;
            }
			else
			{
				m_Agent.isStopped = true;
			}
        }
		#endregion


		#region INHERITED FUNCTIONS
		/// <summary>
		/// Applies the damage specified to the enemy.
		/// </summary>
		/// <param name="damage">Amount of damage to apply.</param>
		/// <param name="damageDir">The direction in which the damage is travelling.</param>
		/// <returns>Whether the damage has destroyed the enemy.</returns>
		public override bool TakeDamage(int damage, Vector3 damageDir = default(Vector3))
		{
			m_Health -= damage;

			m_Agent.velocity = Vector3.zero;

			for (int i = 0; i < damage; i++)
			{
				if (m_ComponentsList.Count > 0)
				{
					Destroy(m_ComponentsList[i]);
					m_ComponentsList.RemoveAt(0);

					SpawnHitEffect(damageDir);
				}
			}

			var isDestroyed = m_Health <= 0;
			if (isDestroyed)
			{
				DestroySelf();
			}

			return isDestroyed;
		}
		#endregion
	}
}
