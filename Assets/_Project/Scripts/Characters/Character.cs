using UnityEngine;

using System;

namespace Hordes
{
    public class Character : MonoBehaviour
    {
        #region EVENTS
        public static event Action<Character> onDestroyed= delegate { };
        public static event Action<Character> onEnslaved = delegate { };
        #endregion


        #region PROPERTIES
        public bool isSlave { get { return m_Components.m_Slave.isActiveAndEnabled; } }
        public bool isInnocent { get { return m_Components.m_Innocent.isActiveAndEnabled; } }
        #endregion


        #region VARIABLES
        public CharacterComponents m_Components;
        #endregion


        #region UNITY EVENTS
        public virtual void OnDestroy()
        {
            onDestroyed(this);
        }
        #endregion


        #region PUBLIC API
        public InnocentCharacter BecomeInnocent()
        {
            m_Components.m_Innocent.gameObject.SetActive(true);
            m_Components.m_Slave.gameObject.SetActive(false);

            return m_Components.m_Innocent;
        }

        public SlaveCharacter BecomeSlave()
        {
            m_Components.m_Innocent.gameObject.SetActive(false);
            m_Components.m_Slave.gameObject.SetActive(true);

            onEnslaved(this);

            return m_Components.m_Slave;
        }

		public virtual void HandleOnTriggerEnter(Collider col) { }
		#endregion
	}
}
