using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Player player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
