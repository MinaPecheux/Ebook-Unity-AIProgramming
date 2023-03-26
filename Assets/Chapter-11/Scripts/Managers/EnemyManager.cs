using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UBAI;

public class EnemyManager : AgentManager
{
    [Header("UBAI")]
    [SerializeField] private UBAIAction[] _actions;

    [Header("UI")]
    [SerializeField] private Image _fireballSlotOverlay;
    [SerializeField] private Image _iceShardSlotOverlay;
    [SerializeField] private Image _healSlotOverlay;

    private MageBrain _brain;

    private void Start()
    {
        _brain = new MageBrain(this, _actions);
    }

    public bool Execute()
    {
        return _brain.Execute();
    }

    public void HighlightSpell(Spell spell)
    {
        Image img = null;
        if (spell == Spell.LIB["Fireball"])      img = _fireballSlotOverlay;
        else if (spell == Spell.LIB["IceShard"]) img = _iceShardSlotOverlay;
        else if (spell == Spell.LIB["Heal"])     img = _healSlotOverlay;

        if (img == null) return;
        StartCoroutine(_HighlightingSpell(img));
    }

    private IEnumerator _HighlightingSpell(Image img)
    {
        img.enabled = true;
        yield return new WaitForSeconds(1f);
        img.enabled = false;
    }
}
