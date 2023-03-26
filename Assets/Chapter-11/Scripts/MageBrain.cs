using UBAI;

public class MageBrain : UBAIBrain
{
    private EnemyManager _agent;

    public MageBrain(EnemyManager agent, UBAIAction[] actions)
        : base(ElectionPolicy.Best, actions)
    {
        _agent = agent;
    }

    private void _CastFireball() { _CastSpell(Spell.LIB["Fireball"]); }
    private void _CastIceShard() { _CastSpell(Spell.LIB["IceShard"]); }
    private void _CastHeal() { _CastSpell(Spell.LIB["Heal"]); }

    private void _CastSpell(Spell spell)
    {
        _agent.Cast(spell);
        _agent.HighlightSpell(spell);
    }

}
