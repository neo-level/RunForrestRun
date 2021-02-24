using UnityEngine;

public class Deactivate : MonoBehaviour
{
    // BUG: The bouncing of the player causes multiple triggers to occur. 
    private bool _deactivationScheduled; // Prevent multiple triggers through bouncing.
    /// <summary>
    /// Sets the platform to inactive after the player touched the trigger with a 5 second delay.
    /// </summary>
    /// <param name="player"></param>
    private void OnCollisionExit(Collision player)
    {
        if (PlayerController.isDead) return;
        
        if (player.gameObject.CompareTag("Player") && !_deactivationScheduled)
        {
            Invoke(nameof(SetInactive), 5.0f);
            _deactivationScheduled = true;
        }
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
        _deactivationScheduled = false;
    }
}