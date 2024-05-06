using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Animator playerAnimator;

    //[Space]
    //[Header("World Objects")]
    //[SerializeField]
    //private List<PlayerPositionController> positions = new List<PlayerPositionController>();


    private void Start()
    {

    }

    public void Flip(bool isLookRight)
    {
        if (isLookRight)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, 180, 0);
    }
}