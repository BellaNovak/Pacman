using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour //assim essa classe nao pode se inicializar sozinha, deve ser inicializado algum dos modos
{
    public Ghost ghost { get; private set; }

    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);

    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke(); //caso esteja no modo com medo e seja comido outro powerpellet
        Invoke(nameof(Disable), duration); //desativa o modo do fantasma quando acaba sua duracao

    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();

    }

  
}
