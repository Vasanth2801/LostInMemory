using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [Header("References")]
    public Player player;
    public SpellUIManager spellUIManager;

    [Header("Spell")]
    [SerializeField] private List<SpellSO> availableSpells = new List<SpellSO>();
    [SerializeField] private int currentSpellIndex = 0;

    public SpellSO CurrentSpell => availableSpells.Count > 0 ? availableSpells[currentSpellIndex] : null;

    [Header("CoolDown")]
    public bool canCastSpell => Time.time >= nextCastTime;
    private float nextCastTime;

    private void Start()
    {
        spellUIManager.ShowSpell(availableSpells);
        HighlightSpell();
    }

    public void LearnSpell(SpellSO spell)
    {
        if(!availableSpells.Contains(spell))
        {
            availableSpells.Add(spell);
        }

        currentSpellIndex = Mathf.Clamp(currentSpellIndex, 0, availableSpells.Count - 1);

        spellUIManager.ShowSpell(availableSpells);

        if(availableSpells.Count > 0)
        {
            HighlightSpell();
        }
    }

    public void HighlightSpell()
    {
        spellUIManager.HighlightSpellBox(CurrentSpell);
    }

    public void NextSpell()
    {
        if(availableSpells.Count == 0)
        {
            return;
        }
        currentSpellIndex = (currentSpellIndex + 1) % availableSpells.Count;
        HighlightSpell();
    }

    public void PreviousSpell()
    {
        if (availableSpells.Count == 0)
        {
            return;
        }
        currentSpellIndex = (currentSpellIndex -1 + availableSpells.Count) % availableSpells.Count;
        HighlightSpell();
    }

    public void AnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }

    void CastSpell()
    {
        if(!canCastSpell || CurrentSpell == null)
        {
            return; 
        }

        CurrentSpell.Cast(player);

        nextCastTime = Time.time + CurrentSpell.cooldownTime;

        spellUIManager.TriggerCooldown(CurrentSpell, CurrentSpell.cooldownTime);
    }
}