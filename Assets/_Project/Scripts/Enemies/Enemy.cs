using System;

using UnityEngine;
using UnityEngine.AI;

namespace Hordes
{
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : MonoBehaviour
	{
		#region EVENTS
		public static Action<Enemy> onEnemyDestroyed = delegate { };
		#endregion 

	
		#region VARIABLES
		public int m_Life = 1;
		public GameObject m_DeathEffect;

        protected NavMeshAgent m_Agent;
        #endregion


        #region UNITY EVENTS
        protected virtual void Start()
		{
			m_Agent = GetComponent<NavMeshAgent>();
		}

		protected abstract void Update();

		private void OnTriggerEnter(Collider col)
		{
			var player = col.GetComponentInChildren<Player>();
			if (player != null)
			{
				player.TakeDamage();
				TakeDamage(1, transform.forward);
			}
		}
		#endregion


		#region PUBLIC API
		public virtual bool TakeDamage(int damage, Vector3 damageDir = default(Vector3))
        {
            m_Life -= damage;

            if (m_Life <= 0)
            {
                DestroySelf(damageDir);
            }

            return false;
        }
        #endregion


        #region HELPER FUNCTIONS
        protected virtual void DestroySelf(Vector3 damageDir = default(Vector3))
        {
			if (m_DeathEffect != null && damageDir != default(Vector3))
			{
				var effect = Instantiate(m_DeathEffect); 
				effect.transform.position = transform.position;
				effect.transform.forward = damageDir;
			}

			Destroy(gameObject);
        }
        #endregion
    }
}
