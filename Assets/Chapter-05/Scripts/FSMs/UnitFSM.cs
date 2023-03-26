using UnityEngine;

using FSM;

public abstract class UnitFSM : StateMachine
{
    [HideInInspector] public Animator animator;

    [Header("Base Properties")]
    public float speed = 2f;
    public float fieldOfVision = 3f;
    public float attackRadius = 1f;
    public float attackRate = 2.4f; // in seconds

    [Header("Health")]
    public Renderer healthbarRenderer;
    public int maxHealth = 10;
    [HideInInspector] protected int _currentHealth;

    private static MaterialPropertyBlock _mpb;
    private static MaterialPropertyBlock _Mpb
    {
        get
        {
            if (_mpb == null) _mpb = new MaterialPropertyBlock();
            return _mpb;
        }
    }

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = maxHealth;
    }

    public bool TakeDamage()
    {
        _currentHealth--;

        healthbarRenderer.GetPropertyBlock(_Mpb);
        _Mpb.SetFloat("_Health", _currentHealth / (float) maxHealth);
        healthbarRenderer.SetPropertyBlock(_Mpb);

        if (_currentHealth <= 0)
        {
            ChangeState(GetState("dying"));
            return true;
        }

        return false;
    }

  protected virtual void OnDrawGizmos()
      {
            //Handles.color = new Color(1f, 1f, 0f, 0.2f);
            //Handles.DrawSolidDisc(transform.position, Vector3.up, fieldOfVision);
            //Handles.color = new Color(1f, 0f, 0f, 0.2f);
            //Handles.DrawSolidDisc(transform.position, Vector3.up, attackRadius);
    }
}
