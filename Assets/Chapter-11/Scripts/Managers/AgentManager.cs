using System.Collections;
using UnityEngine;
using System.Reflection;

using SOMD;

public class AgentManager : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _castVfxParent;
    [SerializeField] protected Transform _hitVfxParent;
    [SerializeField] protected GameObject _freezeMarker;
    [SerializeField] protected AgentManager _opponentManager;

    [Header("Stats")]
    [SerializeField] private IntegerVariable _health;
    [SerializeField] private IntegerVariable _mana;
    [SerializeField] private BoolVariable _frozen;
    private BoolVariable _canCastFireball;
    private BoolVariable _canCastIceShard;
    private BoolVariable _canCastHeal;

    private int _frozenTurns;
    private GameObject _castVfx;

    private void Awake()
    {
        _health.value = _health.max;
        _mana.value = _mana.max;
        _SetFreeze(false);
        _castVfx = null;
    }

    public void StartTurn()
    {
        if (_frozenTurns > 0) _frozenTurns--;
        if (_frozenTurns == 0) _SetFreeze(false);

        if (_canCastFireball) _canCastFireball.value = CanCast(Spell.LIB["Fireball"]);
        if (_canCastIceShard) _canCastIceShard.value = CanCast(Spell.LIB["IceShard"]);
        if (_canCastHeal)     _canCastHeal.value = CanCast(Spell.LIB["Heal"]);
    }

    public void Cast(Spell spell)
    {
        float delay = 0f;
        if (_animator)
        {
            if (_castVfxParent != null && spell.castVfxPrefab != null)
                _castVfx = Instantiate(spell.castVfxPrefab, _castVfxParent.position, Quaternion.identity);
            _animator.SetTrigger(spell.animation);
            delay = 1f;
        }

        StartCoroutine(_Casting(spell, delay));
    }

    public void Heal(Spell spell)
    {
        _health.value += spell.amount;
        _health.updated.Invoke();
        _ShowSpellVFX(spell);
    }

    public void TakeHit(Spell spell)
    {
        if (spell.freezes) _SetFreeze(true);

        _health.value -= spell.amount;
        _health.updated.Invoke();
        if (_health.value <= 0) _Die();
        else if (_animator) _animator.SetTrigger("TakeHit");
        _ShowSpellVFX(spell);
    }

    private IEnumerator _Casting(Spell spell, float delay)
    {
        yield return new WaitForSeconds(delay);

        MethodInfo i = GetType().GetMethod(
            spell.callbackName, BindingFlags.Instance | BindingFlags.Public);
        i.Invoke(spell.onSelf ? this : _opponentManager, new object[] { spell });

        _mana.value -= spell.manaCost + (_frozen.value ? 1 : 0);
        _mana.updated.Invoke();

        yield return new WaitForSeconds(1f);

        if (_castVfx != null)
        {
            Destroy(_castVfx);
            _castVfx = null;
        }
    }

    private void _ShowSpellVFX(Spell spell)
    {
        if (_hitVfxParent != null && spell.hitVfxPrefab != null)
            Instantiate(spell.hitVfxPrefab, _hitVfxParent.position, Quaternion.identity);
    }

    private void _SetFreeze(bool on)
    {
        _frozen.value = on;
        _freezeMarker.SetActive(on);
        if (on) _frozenTurns = 2;
    }

    private void _Die()
    {
        if (_animator) _animator.SetTrigger("Die");
        GameManager.instance.StopGame();
        Invoke("_FinishGame", 3f);
    }

    private void _FinishGame()
    {
        if (this is PlayerManager) GameManager.instance.GameOver();
        else GameManager.instance.Win();
    }

    public void RestoreMana()
    {
        _mana.value += 2;
        _mana.updated.Invoke();
    }

    public bool CanCast(Spell spell)
    {
        return _mana.value >= (spell.manaCost + (_frozen.value ? 1 : 0));
    }
}
