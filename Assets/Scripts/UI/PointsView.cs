using Assets.Scripts.Spawning;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PointsView : MonoBehaviour
    {
        [SerializeField] private LevelSwitcher _levelSwitcher;
        [SerializeField] private Text _pointLabel;

        private void Start()
        {
            _levelSwitcher.ChangedPoints += (int points) =>
            {
                _pointLabel.text = points.ToString();
            };
        }
    }
}