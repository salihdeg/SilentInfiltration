using System.Collections;
using TMPro;
using UnityEngine;

public class CollectibleInfoUI : MonoBehaviour
{
    public static CollectibleInfoUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _warningText;
    [SerializeField] private float timeBtwnChars;
    [SerializeField] private float timeBtwnWords;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.OnDataAdded += UpdateInfoText;
        GameManager.Instance.OnWeaponCaseAdded += UpdateInfoText;

        UpdateInfoText();
    }

    private void UpdateInfoText(object sender, System.EventArgs e)
    {
        UpdateInfoText();
    }

    private void UpdateInfoText()
    {
        string text = "";
        if (GameManager.Instance.currentCollectibleData < GameManager.Instance.maxCollectibleData)
        {
            text += $"Toplanan Veriler: {GameManager.Instance.currentCollectibleData}/{GameManager.Instance.maxCollectibleData}";
        }

        if (GameManager.Instance.currentMilitaryWeaponCase < GameManager.Instance.maxMilitaryWeaponCase)
        {
            text += $"\nGizli Silahlar: {GameManager.Instance.currentMilitaryWeaponCase}/{GameManager.Instance.maxMilitaryWeaponCase}";
        }

        if (text == "")
        {
            text = "Her þeyi topladýn!\nTünelden çýkabilirsin!";
        }

        _infoText.text = text.Trim();
    }

    public void ShowWarningTextAfterHide(string warningText)
    {
        StartCoroutine(WarningTextCoroutine(warningText));
    }

    public IEnumerator WarningTextCoroutine(string warningText)
    {
        _warningText.text = warningText;
        _warningText.gameObject.SetActive(true);
        StartCoroutine(TextVisible());
        yield return new WaitForSeconds(2f);
        _warningText.gameObject.SetActive(false);
    }

    private IEnumerator TextVisible()
    {
        _warningText.ForceMeshUpdate();
        int totalVisibleCharacters = _warningText.textInfo.characterCount;
        int counter = 0;
        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _warningText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                break;
            }
            counter++;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}