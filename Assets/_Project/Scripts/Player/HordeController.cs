using System.Collections.Generic;

using UnityEngine;

namespace Hordes
{
    public class HordeController : MonoBehaviour
    {
        #region PROPERTIES
        private int colCapacity { get { return m_CurrentRow + 1; } }
        #endregion


        #region VARIABLES
        public float m_RowSpacing = 2;
        public float m_ColSpacing = 2;

        private List<Character> m_Horde = new List<Character>();
        private int m_CurrentRow = 1;
        private int m_CurrentCol = 0;
        #endregion


        #region PUBLIC API
        public void Enslave(Character character)
        {
            var slave = character.BecomeSlave();
            slave.Initialise(transform, GetNextPosition());
        }
        #endregion


        #region HELPER FUNCTIONS
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

            if (m_CurrentCol == colCapacity)
            {
                m_CurrentCol = 0;
                m_CurrentRow++;
            }

            return new Vector3(colOffset, 0, rowOffset);
        }
        #endregion
    }
}
