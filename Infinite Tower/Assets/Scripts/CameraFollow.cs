﻿using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraFollow : MonoBehaviour {
    public Transform player;
    public Vector3 offset;

    void Update() {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }

}