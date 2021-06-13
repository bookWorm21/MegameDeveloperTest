using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private Ship.Ship _ship;
        [SerializeField] private GameMenu _gameMenu;

        private void Start()
        {
            _ship.ChangedHealth += (int health) =>
            {
                if(health <= 0)
                {
                    _ship.enabled = false;
                    _gameMenu.OnClickOpenMenu(false);
                }
            };
        }
    }
}