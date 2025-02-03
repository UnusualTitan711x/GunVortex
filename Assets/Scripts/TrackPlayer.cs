using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    Vector3 playerPosition;
    public Vector3 offset;

    void Update()
    {
        playerPosition = PlayerManager.instance.player.transform.position;
        transform.position = playerPosition + offset;
    }
}
