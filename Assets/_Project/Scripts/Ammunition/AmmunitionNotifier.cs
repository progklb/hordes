using System;

using UnityEngine;

namespace Hordes
{
    public class AmmunitionNotifier : MonoBehaviour
    {
        #region EVENTS
        public static event Action<Ammunition> onAmmoTouched = delegate { }; 
        #endregion


        #region UNITY EVENTS
        void OnTriggerEnter(Collider col)
        {
            var ammo = col.gameObject.GetComponentInChildren<Ammunition>();
            if (ammo != null)
            {
                onAmmoTouched(ammo);
            }
        }
        #endregion
    }
}
