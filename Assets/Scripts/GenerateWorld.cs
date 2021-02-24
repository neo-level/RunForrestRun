using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GameObject dummy;
    public static GameObject lastPlatform;

    private static sbyte _stairHeight = 5;
    private static sbyte _platformLength = 10;
    private static sbyte _platformTLength = 20;
    private static sbyte _platformHeight = -10;

    private static byte _stairRotation = 180;


    private void Awake()
    {
        // Use a dummy as a reference point.
        dummy = new GameObject("dummy");
    }

    public static void RunDummy()
    {
        GameObject platform = Pool.singleton.GetRandom();

        if (platform == null) return;


        if (lastPlatform != null)
        {
            // Position the dummy one platform ahead from the last generated one.
            if (lastPlatform.CompareTag("platformTSection"))
            {
                // The length of the T-section is double the size of its counterparts.
                dummy.transform.position = lastPlatform.transform.position +
                                           (PlayerController.player.transform.forward * _platformTLength);
            }
            else
            {
                dummy.transform.position = lastPlatform.transform.position +
                                           (PlayerController.player.transform.forward * _platformLength);
            }

            switch (lastPlatform.tag)
            {
                case "stairsUp":
                    dummy.transform.Translate(Vector3.up * _stairHeight);
                    break;
                case "stairsDown":
                    dummy.transform.Translate(Vector3.down * _stairHeight);
                    platform.transform.Rotate(Vector3.up * _stairRotation);
                    platform.transform.position = dummy.transform.position;
                    break;
            }
        }

        // record the last platform that we want to place down.
        lastPlatform = platform;
        platform.SetActive(true);
        platform.transform.position = dummy.transform.position;
        platform.transform.rotation = dummy.transform.rotation;

        dummy.transform.Translate(Vector3.forward * _platformHeight);
    }
}