using System;

using UnityEngine;

namespace Hordes
{
    public class CharacterNotifier : MonoBehaviour
    {
        #region EVENTS
        public static event Action<Character> onCharacterTouched = delegate { }; 
        #endregion


        #region UNITY EVENTS
        void OnTriggerEnter(Collider col)
        {
            var character = col.gameObject.GetComponentInChildren<Character>();
            if (character != null)
            {
                onCharacterTouched(character);
            }
        }
        #endregion
    }
}
