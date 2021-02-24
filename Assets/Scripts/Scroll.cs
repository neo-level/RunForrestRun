using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float _gameSpeed = -5.0f;
    private float _stairsAngle = 0.06f;  // the -0.06f comes from the stairs angle.
    private void FixedUpdate()
    {
        transform.position += PlayerController.player.transform.forward * (_gameSpeed * Time.deltaTime);

        if (PlayerController.currentPlatform == null) return;
        
        // Moves the player/environment up or down respectively.
        if (PlayerController.currentPlatform.CompareTag("stairsUp"))
            transform.Translate(0, y:-_stairsAngle, 0);  
        
        if (PlayerController.currentPlatform.CompareTag("stairsDown"))
            transform.Translate(0, y:_stairsAngle, 0);
    }
}
