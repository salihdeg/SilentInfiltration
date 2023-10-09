using System.Collections;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _doorClosedAngle;
    [SerializeField] private Vector3 _doorOpenedAngle;
    [SerializeField] private float _doorSpeed = 3f;
    [SerializeField] private bool _isOpen;

    public string GetInteractText()
    {
        return "Open/Close Door";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
        if (_isOpen)
        {
            StartCoroutine(ChangeDoorAngle(_doorClosedAngle));
        }
        else
        {
            StartCoroutine(ChangeDoorAngle(_doorOpenedAngle));
        }
    }

    private IEnumerator ChangeDoorAngle(Vector3 targetAngle)
    {
        // Loop until we reach the target angle or get very close
        while (Mathf.Abs(transform.localEulerAngles.y - targetAngle.y) > 1f)
        {
            float t = _doorSpeed * Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetAngle, t);
            yield return null;
        }

        // Ensure we set the exact target angle to avoid small differences
        transform.localEulerAngles = targetAngle;

        _isOpen = !_isOpen;
    }
}
