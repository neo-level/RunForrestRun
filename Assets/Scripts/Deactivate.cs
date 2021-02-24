using UnityEngine;

public class Deactivate : MonoBehaviour
{
    /// <summary>
    /// Sets the platform to inactive after the player touched the trigger with a 5 second delay.
    /// </summary>
    /// <param name="player"></param>
    private void OnCollisionExit(Collision player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(SetInactive), 5.0f);
        }
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}