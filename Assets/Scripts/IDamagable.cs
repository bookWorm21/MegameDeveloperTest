using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IDamagable
    {
        public void ApplyDamage(int damage);
    }
}