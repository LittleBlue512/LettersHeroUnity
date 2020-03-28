using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public bool FollowPlayer;
    public float offset = 10f;

    void Start()
    {
        mainCamera.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    void Update()
    {
        if (FollowPlayer)
            mainCamera.transform.position = player.transform.position + new Vector3(0, offset, 0);
    }
}
