using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 touchPos;
    public TouchState touchState;
    public enum TouchState
    {
        Began,
        Moved,
        Ended
    }
    public bool IsDragging;
}