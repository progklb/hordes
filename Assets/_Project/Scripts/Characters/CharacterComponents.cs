using UnityEngine;

namespace Hordes
{
    public class CharacterComponents : MonoBehaviour
    {
        #region VARIABLES
        public SlaveCharacter m_Slave;
        public InnocentCharacter m_Innocent;

        public Rigidbody m_Body;
		#endregion


        #region UNITY EVENTS
		void OnTriggerEnter(Collider col)
		{
			m_Slave.HandleOnTriggerEnter(col);
			m_Innocent.HandleOnTriggerEnter(col);
		}
        #endregion
	}
}

