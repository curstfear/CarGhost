using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        if (_boxCollider != null)
        {
            EventBus.Instance.GameStarted += EnableCollider;
            EventBus.Instance.PlayerFinished += DisableCollider;
        }
    }

    private void OnDisable()
    {
        EventBus.Instance.GameStarted -= EnableCollider;
        EventBus.Instance.PlayerFinished -= DisableCollider;
    }

    private void EnableCollider()
    {
        if (_boxCollider != null)
        {
            _boxCollider.enabled = true;
        }
    }

    private void DisableCollider()
    {
        if (_boxCollider != null)
        {
            _boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.Instance.PlayerFinished?.Invoke();
        }
    }
}