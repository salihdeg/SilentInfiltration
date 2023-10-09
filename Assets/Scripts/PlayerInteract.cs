using Climbing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private bool _debug;

    [Header("PROPERTIES")]
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _interactRange = 2f;

    private InputCharacterController _inputController;

    private void Awake()
    {
        _inputController = GetComponent<InputCharacterController>();
    }

    private void Start()
    {
        _inputController.OnInteractAction += InputController_OnInteractAction;
    }

    private void InputController_OnInteractAction(object sender, System.EventArgs e)
    {
        IInteractable interactable = GetInteractableObject();

        interactable?.Interact();
    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(_interactPoint.position, _interactRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;

        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(_interactPoint.position, interactable.GetTransform().position) < Vector3.Distance(_interactPoint.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }

    private void OnDrawGizmos()
    {
        if (_interactPoint == null) return;
        if (!_debug) return;

        Color color = Color.white;
        color.a = 0.2f;
        Gizmos.color = color;
        Gizmos.DrawSphere(_interactPoint.position, _interactRange);
    }
}
