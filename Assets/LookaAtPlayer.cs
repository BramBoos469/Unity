using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    public Transform shoebill, player;

    void Update()
    {
        shoebill.transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}