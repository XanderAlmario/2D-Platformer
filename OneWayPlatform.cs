using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    public PlayerControl playerControl;
    public PlatformEffector2D platformEffector2D;

    private void Awake()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerControl = collision.gameObject.GetComponent<PlayerControl>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerControl == null)
        {
            return;
        }
        else if (playerControl.fallThrough)
        {
            platformEffector2D.rotationalOffset = 180;
            playerControl = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerControl = null;
        platformEffector2D.rotationalOffset = 0;
    }
}
