using UnityEngine;

public enum CollectibleType
{
    Information,
    WeaponCase
}

public class CollectibleInterract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactText = "Steal Information/Open Case";
    [SerializeField] private string _messageTextOnCollected;
    [SerializeField] private bool _hasCollectible;
    [SerializeField] private CollectibleType _collectibleType;

    public string GetInteractText()
    {
        return _interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
        if (_hasCollectible)
        {
            CollectibleInfoUI.Instance.ShowWarningTextAfterHide(_messageTextOnCollected);

            if (_collectibleType == CollectibleType.Information)
                GameManager.Instance.AddData();
            else if(_collectibleType == CollectibleType.WeaponCase)
                GameManager.Instance.AddWeaponCase();
        }
        else
        {
            CollectibleInfoUI.Instance.ShowWarningTextAfterHide("Aradýðým þey bunda yok.");
        }
        DeleteMapIcon();
        Destroy(this);
    }

    private void DeleteMapIcon()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
