using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    GameObject player;

    public void SetCamera(GameObject player)
    {
        this.player = player;
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime);
        transform.LookAt(player.transform);
    }

}
