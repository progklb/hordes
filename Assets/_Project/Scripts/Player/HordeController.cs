using System.Collections.Generic;

using UnityEngine;

namespace Hordes
{
    public class HordeController : MonoBehaviour
    {
        #region CONSTANTS
        private const int STARTING_ROW = 1;
        private const int STARTING_COL = 0;
        #endregion


        #region PROPERTIES
        private int colCapacity { get { return m_CurrentRow + 1; } }
		private SlaveCharacter lastSlave { get { return m_Horde.Count > 0 ? m_Horde[m_Horde.Count - 1].m_Components.m_Slave : null; } }
        #endregion


        #region VARIABLES
        public float m_RowSpacing = 1;
        public float m_ColSpacing = 1;

        private List<Character> m_Horde = new List<Character>();
        private int m_CurrentRow = STARTING_ROW;
        private int m_CurrentCol = STARTING_COL;

		private bool m_AttackReady = false;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            Character.onDestroyed += HandleSlavedDestroyed;
        }
        #endregion


        #region PUBLIC API
        public void Enslave(Character character)
        {
            var slave = character.BecomeSlave();
            m_Horde.Add(character);

            ResetFormation();
		}
		#endregion


		#region PUBLIC API - ATTACKING
		public void SetAttackReady(bool ready)
		{
			m_AttackReady = ready;

			foreach (var slave in m_Horde)
			{
				slave.m_Components.m_Slave.SetAttackReady(ready);
			}

			if (lastSlave != null)
			{
				lastSlave.SetAttackSet(ready);
			}
		}

		public void Attack(Vector3 targetPos)
		{
			if (m_AttackReady)
			{
				// Shoot
				if (lastSlave != null)
				{
					lastSlave.Attack(targetPos);
					m_Horde.RemoveAt(m_Horde.Count - 1);
					RecalculatePreviousPosition();

					if (lastSlave != null)
					{
						// Select next slave for shooting
						lastSlave.SetAttackSet(true);
					}
				}
			}
		}
		#endregion


		#region HELPER FUNCTIONS
        void HandleSlavedDestroyed(Character slave)
        {
            m_Horde.Remove(slave);
            ResetFormation();
        }
        #endregion


        #region HELPER FUNCTIONS - FORMATION
		void RecalculatePreviousPosition()
		{
			if (m_CurrentCol > 0)
			{
				m_CurrentCol--;
			}
			else
			{
				m_CurrentRow--;
				m_CurrentCol = colCapacity - 1;
			}
		}

		/// <summary>
		/// Returns a vector that represents the offset to a target body by following it in a delta formation.
		/// </summary>
		/// <returns>Offset to parent.</returns>
		Vector3 GetNextPosition()
        {
			var rowOffset = -(m_CurrentRow * m_RowSpacing);

            var rowWidth = (colCapacity - 1) * m_ColSpacing;
            var rowStart = -(rowWidth / 2);

            var colOffset = rowStart + (m_CurrentCol++ * m_ColSpacing);

            if (m_CurrentCol >= colCapacity)
            {
                m_CurrentCol = 0;
                m_CurrentRow++;
            }

            return new Vector3(colOffset, 0, rowOffset);
        }

        void ResetFormation()
        {
            m_CurrentRow = STARTING_ROW;
            m_CurrentCol = STARTING_COL;

            foreach (var slave in m_Horde)
            {
                slave.m_Components.m_Slave.Initialise(transform, GetNextPosition());

                lastSlave.SetAttackReady(m_AttackReady);
                lastSlave.SetAttackSet(m_AttackReady && slave == lastSlave);
            }
        }
        #endregion
    }
}
