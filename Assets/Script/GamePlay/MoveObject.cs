
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    private ObjectType type;
    public ObjectType Type { get => type; set => type = value; }
}

[System.Serializable]
public enum ObjectType
{
    GiftBox,
    Cake
}
