using UnityEngine;
using System.Collections.Generic;

public class SpellUIManager : MonoBehaviour
{
    [SerializeField] private List<SpellBox> spellBoxes = new List<SpellBox>();

    public void ShowSpell(List<SpellSO> spells)
    {
        for(int i = 0;i< spellBoxes.Count;i++)
        {
            if(i < spells.Count)
            {
                spellBoxes[i].SetSpell(spells[i]);
            }
            else
            {
                spellBoxes[i].SetSpell(null);
            }
        }
    }

    public void HighlightSpellBox(SpellSO activeSpell)
    {
        foreach(SpellBox spellBox in spellBoxes)
        {
            spellBox.HighlightBox(spellBox.AssignedSpell == activeSpell);
        }
    }
}