using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public static GameObject dummy;
    public static GameObject lastPlatform;

    private static sbyte _platformLength = 10;  

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
            dummy.transform.position = lastPlatform.transform.position + 
                                       (PlayerController.player.transform.forward * _platformLength);
        }

        // record the last platform that we want to place down.
        lastPlatform = platform;
        platform.SetActive(true);
        platform.transform.position = dummy.transform.position;
        platform.transform.rotation = dummy.transform.rotation;
    }
}