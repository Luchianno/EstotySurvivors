using UnityEngine;

public abstract class BasePausableBehaviour : MonoBehaviour, IPausable
{
    public virtual void SetPaused(bool isPaused)
    {
        this.enabled = !isPaused;
    }
}
