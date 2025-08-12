using System;
using UnityEngine;

[Serializable]
public class GhostData
{
    public Vector3[] positions;
    public Quaternion[] rotations;
    public float[] timeStamps;

    public GhostData(Vector3[] positions, Quaternion[] rotations, float[] timeStamps)
    {
        this.positions = positions;
        this.rotations = rotations;
        this.timeStamps = timeStamps;
    }
}