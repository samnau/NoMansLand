using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHeroController : MonoBehaviour
{
    BaseMonster baseMonster;
    BattleCombo currentCombo;
    PositionTweener positionTweener;
    [SerializeField] GameObject heroProfile;
    Animator heroProfileAnimator;
    IEnumerator TriggerWalkIn()
    {
        heroProfileAnimator.SetBool("WALK_IN", true);
        heroProfileAnimator.SetBool("AFRAID", false);
        SceneWalkIn();
        yield return new WaitForSeconds(1f);
        heroProfile?.GetComponent<Animator>().SetBool("WALK_IN", false);

    }

    IEnumerator TriggerFear()
    {
        heroProfileAnimator.SetBool("AFRAID", true);
        yield return new WaitForSeconds(.1f);
        heroProfileAnimator.SetBool("AFRAID", false);
    }

    public void ShowFear()
    {
        StartCoroutine("TriggerFear");
    }
    public void SceneWalkIn()
    {
        heroProfileAnimator.SetBool("WALK_IN", true);
        Transform transform = this.transform;
        Vector3 endPosition = transform.position;
        Vector3 startPosition = new Vector3(endPosition.x - 3f, endPosition.y, endPosition.z);
        this.transform.position = startPosition;
        positionTweener?.TriggerPositionByDuration(endPosition, 3.45f);
    }
    public void AnnounceCombo()
    {
        //print("Defend with the combo!");
        UpdateCombo();
        //print($"Molly yells out: {currentCombo.keyCode1} + {currentCombo.keyCode2}");
    }
    // NOTE: this is where the battle intro sequence is triggered
    void Start()
    {
        baseMonster = FindObjectOfType<BaseMonster>();
        //UpdateCombo();
        positionTweener = this.GetComponent<PositionTweener>();
        heroProfileAnimator = heroProfile?.GetComponent<Animator>();
        StartCoroutine(TriggerWalkIn());
    }

    public void UpdateCombo()
    {
        if(baseMonster.defenseCount < baseMonster.defenseCombos.Count)
        {
            currentCombo = baseMonster.defenseCombos[baseMonster.defenseCount];
        }
    }

    // NOTE: can be removed?
    public void StartCounter()
    {
        currentCombo = baseMonster.counterCombos[baseMonster.counterCount];
        AnnounceCombo();
    }

}
