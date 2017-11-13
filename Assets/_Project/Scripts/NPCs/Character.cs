using UnityEngine;

namespace Hordes
{
    public class Character : MonoBehaviour
    {
        #region PROPERTIES
        public bool isSlave { get { return m_Components.m_Slave.isActiveAndEnabled; } }
        public bool isInnocent { get { return m_Components.m_Innocent.isActiveAndEnabled; } }
        #endregion


        #region VARIABLES
        public CharacterComponents m_Components;
        #endregion


        #region UNITY EVENTS
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

            return m_Components.m_Slave;
        }
        #endregion
    }
}
