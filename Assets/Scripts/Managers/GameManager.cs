using Cinemachine;
using Climbing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event System.EventHandler OnDataAdded;
    public event System.EventHandler OnWeaponCaseAdded;

    public bool isGameLose = false;

    public int maxCollectibleData = 6;
    public int currentCollectibleData;
    public int maxMilitaryWeaponCase = 4;
    public int currentMilitaryWeaponCase;

    [SerializeField] private ThirdPersonController _thirdPersonController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _thirdPersonController = FindAnyObjectByType<ThirdPersonController>();
    }

    public void AddData()
    {
        currentCollectibleData++;
        OnDataAdded?.Invoke(this, System.EventArgs.Empty);
    }

    public void AddWeaponCase()
    {
        currentMilitaryWeaponCase++;
        OnWeaponCaseAdded?.Invoke(this, System.EventArgs.Empty);
    }

    public bool IsAllCollectiblesCollected()
    {
        if (currentCollectibleData >= maxCollectibleData && currentMilitaryWeaponCase >= maxMilitaryWeaponCase)
            return true;
        else
            return false;
    }

    public void StopGame()
    {
        _thirdPersonController.DisableController();
        Cursor.lockState = CursorLockMode.None;
        CinemachineInputProvider camInput = FindAnyObjectByType<CinemachineInputProvider>();
        if (camInput != null) camInput.enabled = false;
    }

    public void RestartGame()
    {
        GameRestartManager.isRestart = true;
        currentCollectibleData = 0;
        currentMilitaryWeaponCase = 0;
    }

    public void WinGame()
    {
        EndConditionsUI.Instance.ShowWinPanel();
        _thirdPersonController.DisableController();
        Cursor.lockState = CursorLockMode.None;
        CinemachineInputProvider camInput = FindAnyObjectByType<CinemachineInputProvider>();
        if (camInput != null) camInput.enabled = false;
    }
}
