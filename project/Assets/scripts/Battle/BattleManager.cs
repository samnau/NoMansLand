using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject hero;
    [SerializeField] GameObject heroWrapper;
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject familiar;
    [SerializeField] GameObject monster;
    [SerializeField] GameObject scenery;
    Animator heroAnimator;
    RuneIntroSequencer introSequencer;
    InputStateTracker inputStateTracker;
    [SerializeField] GameEvent familiarTakeDamage;
    [SerializeField] GameEvent startBattle;
    SpriteColorSetter spriteColorSetter;
    Color damageOverlayColor;


    GameObject[] battleChallenges = new GameObject[3];
    bool showBattleUI = false;

    void Start()
    {
        heroAnimator = hero.GetComponent<Animator>();
        introSequencer = battleUI.GetComponentInChildren<RuneIntroSequencer>();
        inputStateTracker = FindObjectOfType<InputStateTracker>();
        spriteColorSetter = this.GetComponent<SpriteColorSetter>();
        damageOverlayColor = new Color(.7f, .15f, .17f, 1f);
        StartCoroutine("StartBattleSequence");
        //StartCoroutine(ShowCriticalDamage(scenery, damageOverlayColor));
    }

    IEnumerator StartBattleSequence()
    {
        yield return new WaitForSeconds(.25f);
        inputStateTracker.isUiActive = true;
        yield return new WaitForSeconds(1.5f);
        // NOTE: current home for the battle scene intro kickoff
        heroAnimator.SetBool("BATTLE_START", true);
        yield return new WaitForSeconds(.25f);
        heroAnimator.SetBool("BATTLE_START", false);
    }

    IEnumerator CriticalDamageFlash(GameObject targetParent, Color targetColor)
    {
        spriteColorSetter.TurnColor(targetParent, targetColor);
        yield return new WaitForSeconds(.25f);
        spriteColorSetter.TurnNormal(targetParent);
    }

    IEnumerator DamageFlash(GameObject targetParent, Color targetColor)
    {
        spriteColorSetter.TurnColor(targetParent, targetColor);
        yield return new WaitForSeconds(.25f);
        spriteColorSetter.TurnNormal(targetParent);
    }

    public void ShowFamiliarDamage()
    {
        StartCoroutine(DamageFlash(familiar, damageOverlayColor));
    }

    public void ShowMonsterDamage()
    {
        StartCoroutine(DamageFlash(monster, damageOverlayColor));
    }
    public void ShowCriticalDamage()
    {

        if(spriteColorSetter)
        {
            //spriteColorSetter.targetParent = targetParent;
            //spriteColorSetter.InitColorSetter();
            // yield return new WaitForSeconds(20f);
            print("damage flash!");
            StartCoroutine(CriticalDamageFlash(heroWrapper, damageOverlayColor));
            StartCoroutine(CriticalDamageFlash(scenery, damageOverlayColor));
            StartCoroutine(CriticalDamageFlash(familiar, Color.black));
            StartCoroutine(CriticalDamageFlash(monster, Color.black));

            //spriteColorSetter.TurnColor(targetParent,targetColor);
            //yield return new WaitForSeconds(.25f);
            //spriteColorSetter.TurnNormal(targetParent);
        }
    }

}
