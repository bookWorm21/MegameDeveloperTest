using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapBorder : MonoBehaviour
    {
        public event System.Action<Transform> Triggered;

        private void OnTriggerStay(Collider collision)
        {
            Triggered?.Invoke(collision.transform);
        }
    }
}