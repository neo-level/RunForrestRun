using UnityEngine;

public class Deactivate : MonoBehaviour {
    
    private void OnCollisionExit(Collision player) {

        if (player.gameObject.CompareTag("Player")) {

            Invoke(nameof(SetInactive), 5.0f);
        }
    }

    private void SetInactive() {
        gameObject.SetActive(false);
    }
}
