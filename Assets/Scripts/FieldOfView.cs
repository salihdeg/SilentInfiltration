using Climbing;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfView : MonoBehaviour
{
    public float raidus;
    [Range(0, 360)] public float angle;
    public float turnSpeedPlayerIsRunning = 1;

    public GameObject playerRef;
    [SerializeField] private GameObject _eyeImage;

    public LayerMask targetLayerMask;
    public LayerMask obsturctionLayerMask;
    public Transform eyePosForRaycast;

    public bool canSeePlayer;

    public float maxInspectTime = 4f;
    [SerializeField] private float _inspectTime;
    [SerializeField] private Image _detectionBar;

    [SerializeField] private InputCharacterController _characterController;

    private void Start()
    {
        playerRef = GameObject.Find("PlayerModel");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        InspectIfCanSeePlayer();

        UpdateBar();
    }

    private void InspectIfCanSeePlayer()
    {
        if (canSeePlayer)
        {
            if (_characterController != null)
            {
                if (_characterController.run) //If player is running inspect speed = 2x (add time one more time)
                {
                    _inspectTime += Time.deltaTime;
                }
                else if (_characterController.crouch) //If player is crouching inspect speed = 1/2x (increase slowly)
                {
                    _inspectTime += Time.deltaTime / 2;
                }
                else //If player is not crouching or running inspect normal
                {
                    _inspectTime += Time.deltaTime;
                }
            }

            if (_inspectTime >= maxInspectTime)
            {
                //TODO: Yakalanma durumu þartlarýný yaz!
                EndConditionsUI.Instance.ShowCaughtPanel();
                GameManager.Instance.StopGame();
                Debug.Log("YAKALANDIN!");
            }
        }
        else if (_inspectTime > 0f) //If cannot see player, decrease inspecttime slowly
        {
            _inspectTime -= Time.deltaTime;
            if (_inspectTime < 0f) _inspectTime = 0f;
        }
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, raidus, targetLayerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            target.TryGetComponent(out _characterController);

            _eyeImage.SetActive(true);

            // If player in range and "is running" look at player!
            //if (_characterController.run)
            //{
            //    Debug.Log("Bakmaya çalýþýyorum");
            //    SmoothLookAt(target.position);
            //}

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(eyePosForRaycast.position, directionToTarget, distanceToTarget, obsturctionLayerMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
        else if (_detectionBar.fillAmount == 0) //If cannot see player and detect bar is empty too, close eye image
        {
            _eyeImage.SetActive(false);
        }
    }

    private void UpdateBar()
    {
        if (_detectionBar == null) return;

        _detectionBar.fillAmount = ((_inspectTime * 100) / maxInspectTime) / 100;
    }
    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        //direction.x = 0;
        //direction.z = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeedPlayerIsRunning * Time.deltaTime);
    }
}
