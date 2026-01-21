using UnityEngine;
using UnityEngine.UI;

public class SpellBox : MonoBehaviour
{
    public Image iconImage;
    public GameObject highlightObject;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;
    Vector3 normalScale = new Vector3(1f, 1f, 1f);
    Vector3 highlightScale = new Vector3(1.2f, 1.2f, 1.2f);

    public SpellSO AssignedSpell { get; private set; }

    public void SetSpell(SpellSO spellSO)
    {
        if(spellSO != null)
        {
            AssignedSpell = spellSO;

            if(spellSO != null)
            {
                iconImage.sprite = spellSO.itemIcon;
                iconImage.gameObject.SetActive(true);
            }
            else
            {
                iconImage.sprite = null;
                iconImage.gameObject.SetActive(false);
            }
            HighlightBox(false);
        }
    }

    public void HighlightBox(bool active)
    {
        highlightObject.SetActive(active);
        iconImage.color = active ? highlightColor : normalColor;
        iconImage.transform.localScale = active ? highlightScale : normalScale;
    }
}