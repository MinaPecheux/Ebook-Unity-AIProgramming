using UnityEngine;

public class PlayerManager : AgentManager
{
    void Update()
    {
        if (!GameManager.instance.IAmPlaying) return;

        if (Input.GetKeyDown(KeyCode.A))            _CastFireball();
        else if (Input.GetKeyDown(KeyCode.Z))       _CastIceShard();
        else if (Input.GetKeyDown(KeyCode.E))       _CastHeal();
        else if (Input.GetKeyDown(KeyCode.Space))   _SkipTurn();
    }

    public void CastSpell(Spell spell)
    {
        Cast(spell);
        Invoke("_EndTurn", 1f);
    }

    private void _CastFireball() {
        if (!CanCast(Spell.LIB["Fireball"])) return;
        Cast(Spell.LIB["Fireball"]);
        _EndTurn();
    }
    private void _CastIceShard()
    {
        if (!CanCast(Spell.LIB["IceShard"])) return;
        Cast(Spell.LIB["IceShard"]);
        _EndTurn();
    }
    private void _CastHeal()
    {
        if (!CanCast(Spell.LIB["Heal"])) return;
        Cast(Spell.LIB["Heal"]);
        _EndTurn();
    }

    private void _SkipTurn()
    {
        _EndTurn();
    }

    private void _EndTurn()
    {
        GameManager.instance.EndTurn();
    }
}
