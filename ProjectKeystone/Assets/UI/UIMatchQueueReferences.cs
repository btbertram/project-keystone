using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script for the "MatchQueueRootPanel" Prefab object. UI objects/Components that change dynamically during gameplay
/// are exposed and set in the inspector to be able to easily change them at runtime.
/// </summary>
public class UIMatchQueueReferences : MonoBehaviour
{
    public GameObject QueueSpot0ImageGameObject;
    public GameObject QueueSpot1ImageGameObject;
    public GameObject QueueSpot2ImageGameObject;
    public GameObject QueueSpot3ImageGameObject;

    Image[] _queueImageArray;
    public Image[] QueueImages {get => _queueImageArray;}


    void Awake()
    {
        List<Image> images = new List<Image>(4);

        images.Add(QueueSpot0ImageGameObject.GetComponent<Image>());
        images.Add(QueueSpot1ImageGameObject.GetComponent<Image>());
        images.Add(QueueSpot2ImageGameObject.GetComponent<Image>());
        images.Add(QueueSpot3ImageGameObject.GetComponent<Image>());

        _queueImageArray = images.ToArray();
    }
}
