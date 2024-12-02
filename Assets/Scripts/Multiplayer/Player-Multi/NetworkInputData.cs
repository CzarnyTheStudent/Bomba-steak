using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 DragStart;
    public Vector3 DragEnd;
    public bool IsDragging;
}