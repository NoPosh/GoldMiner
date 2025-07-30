using UnityEngine;

public abstract class NpcCapabilitySO : ScriptableObject
{
    public abstract INpcCapability CreateInstance();
}
