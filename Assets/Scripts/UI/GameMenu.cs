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
        [SerializeField] private GameObject _gameMenuPanel;
        [SerializeField] private CurrentInputSource _currentInputSource;
        [SerializeField] private int _arcadaGameSceneBuildNumber;
        [SerializeField] private Text _currentInputSourceText;
        [SerializeField] private GameObject _continueButton;

        private bool _isOpen;

        private void OnEnable()
        {
            _currentInputSource.ChangedInputing += OnChangeInputting;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(_isOpen)
                {
                    OnClickContinueButton();
                }
                else
                {
                    OnClickOpenMenu(true);
                }
            }
        }

        private void OnDisable()
        {
            _currentInputSource.ChangedInputing -= OnChangeInputting;
        }

        public void OnClickOpenMenu(bool alivePlayer)
        {
            _continueButton.SetActive(alivePlayer);
            _currentInputSource.DisableInput();
            _isOpen = true;
            _gameMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }

        public void OnClickContinueButton()
        {
            _currentInputSource.EnableInput();
            _gameMenuPanel.SetActive(false);
            _isOpen = false;
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