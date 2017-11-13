using UnityEngine;

namespace Hordes
{
    public class PlayerController : MonoBehaviour
    {
        #region VARIABLES
        public HordeController m_HordeController;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            CharacterNotifier.onCharacterTouched += OnCharacterTouched;
        }
        #endregion


        #region EVENT HANDLERS
        void OnCharacterTouched(Character character)
        {
            if (character.isInnocent)
            {
                m_HordeController.Enslave(character);
            }
        }
        #endregion
    }
}
