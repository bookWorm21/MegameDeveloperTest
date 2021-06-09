using Assets.Scripts.Inputing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private CurrentInputSource _currentInputSource;
        [SerializeField] private int _arcadaGameSceneBuildNumber;
        [SerializeField] private Text _currentInputSourceText;

        private void OnEnable()
        {
            _currentInputSource.ChangedInputing += OnChangeInputting;
        }

        private void OnDisable()
        {
            _currentInputSource.ChangedInputing -= OnChangeInputting;
        }

        public void OnClickGameMenuButton()
        {
            Time.timeScale = 0;
        }

        public void OnClickContinueButton()
        {
            Time.timeScale = 1;
        }

        public void OnClickNewGameButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_arcadaGameSceneBuildNumber);
        }

        public void OnClickChangeInputControl()
        {
            _currentInputSource.ChangeInputting();
            _currentInputSourceText.text = _currentInputSource.CurrentInputtingName;
        }

        public void OnClickExitButton()
        {
            Time.timeScale = 1;
            Application.Quit();
        }

        private void OnChangeInputting(ShipInputting inputting)
        {
            _currentInputSourceText.text = inputting.GetViewName();
        }
    }
}