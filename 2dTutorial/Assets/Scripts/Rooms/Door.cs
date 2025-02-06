using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform roomToActivate;
    [SerializeField] private Transform roomToDeactivate;
    [SerializeField] private CameraController cam;
    private Coroutine continuousActionCoroutine;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (continuousActionCoroutine == null)
            {
                continuousActionCoroutine = StartCoroutine(ContinuosActiveDisable());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (continuousActionCoroutine != null)
            {
                StopCoroutine(continuousActionCoroutine);
                continuousActionCoroutine = null;
            }
        }
    }

    private IEnumerator ContinuosActiveDisable()
    {
        while (true)
        {
            cam.MoveToNewRoom(roomToActivate);
            if (!roomToActivate.GetComponent<Room>().Active)
            {
                roomToActivate.GetComponent<Room>().ActivateRoom(true);
            }
            roomToDeactivate.GetComponent<Room>().ActivateRoom(false);
            yield return null;
        }
    }
}
