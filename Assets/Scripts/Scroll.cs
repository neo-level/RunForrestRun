using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private sbyte _gameSpeed = -5;
    private float _stairsAngle = 0.06f; // the -0.06f comes from the stairs angle.

    private void FixedUpdate()
    {
        transform.position += PlayerController.player.transform.forward * (_gameSpeed * Time.deltaTime);

        if (PlayerController.currentPlatform == null) return;

        // Moves the world up or down respectively.
        switch (PlayerController.currentPlatform.tag)
        {
            case "stairsUp":
                transform.Translate(0, y: -_stairsAngle, 0);
                break;
            case "stairsDown":
                transform.Translate(0, y: _stairsAngle, 0);
                break;
        }
    }
}