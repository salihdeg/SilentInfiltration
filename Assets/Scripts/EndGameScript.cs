using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.IsAllCollectiblesCollected())
        {
            GameManager.Instance.WinGame();
        }
        else
        {
            CollectibleInfoUI.Instance.ShowWarningTextAfterHide("Daha görevimi bitirmedim!");
        }
    }
}
