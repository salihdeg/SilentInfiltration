using Cinemachine;
using Climbing;
using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    [SerializeField] private GameObject _cinematicComponents;
    [SerializeField] private CinemachineDollyCart _dollyCart;
    [SerializeField] private bool DEBUG = false;

    private void Start()
    {
        if (DEBUG) // In Development Mod, skip cinematic
        {
            EndCinematic();
            return;
        }

        if (!GameRestartManager.isRestart)
        {
            _thirdPersonController.DisableController();
            AudioManager.Instance.Play("Start");
            StartCoroutine(CheckCartIsEnd());
        }
        else
        {
            EndCinematic();
        }
    }

    public void EndCinematic()
    {
        _thirdPersonController.EnableController();
        _cinematicComponents.SetActive(false);
    }

    private IEnumerator CheckCartIsEnd()
    {
        while (_dollyCart.m_Position < 232)
        {
            yield return null;
        }
        EndCinematic();
    }
}
