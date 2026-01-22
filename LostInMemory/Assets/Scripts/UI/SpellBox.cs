using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellBox : MonoBehaviour
{
    [Header("UI References")]
    public Image iconImage;
    public GameObject highlightObject;
    [SerializeField] private TextMeshProUGUI spellText;
    [SerializeField] private Image coolDownOverlay;

    [Header("Highlight Settings")]
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
                coolDownOverlay.sprite = spellSO.itemIcon;
                iconImage.sprite = spellSO.itemIcon;
                iconImage.gameObject.SetActive(true);
            }
            else
            {
                iconImage.sprite = null;
                iconImage.gameObject.SetActive(false);
            }
            HighlightBox(false);
            coolDownOverlay.fillAmount = 0f;
        }
    }

    public void HighlightBox(bool active)
    {
        highlightObject.SetActive(active);
        iconImage.color = active ? highlightColor : normalColor;
        iconImage.transform.localScale = active ? highlightScale : normalScale;

        if(AssignedSpell != null)
        {
            spellText.text = AssignedSpell.itemName;
        }
        else
        {
            spellText.text = "";
        }

        spellText.enabled = active;
    }

    public void TriggerCooldown(float coolDownTime)
    {
        StartCoroutine(CoolDownRoutine(coolDownTime));
    }

    private IEnumerator CoolDownRoutine(float duration)
    {
        coolDownOverlay.fillAmount = 1f;
        float elapsed = 0f;

        if(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            coolDownOverlay.fillAmount = (1f - (elapsed / duration));
            yield return null;
        }
        coolDownOverlay.fillAmount = 0f;
    }
}