using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

namespace Hordes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour 
    {
        #region VARIABLES
        public int m_Life = 2;
        private NavMeshAgent m_Agent;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            m_Agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (PlayerController.instance != null)
            {
                m_Agent.destination = PlayerController.instance.transform.position;
            }
        }
        #endregion


        #region PUBLIC API
        public bool TakeDamage(int damage)
        {
            m_Life -= damage;

            if (m_Life <= 0)
            {
                DestroySelf();
            }

            return false;
        }
        #endregion


        #region HELPER FUNCTIONS
        void DestroySelf()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
