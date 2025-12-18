using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-2, player.transform.position.z-4);
    }
}
