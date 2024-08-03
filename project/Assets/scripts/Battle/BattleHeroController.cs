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
        heroProfileAnimator.SetBool("BATTLE", true);
        Transform transform = this.transform;
        Vector3 endPosition = transform.position;
        Vector3 startPosition = new Vector3(endPosition.x - 3f, endPosition.y, endPosition.z);
        this.transform.position = startPosition;
        positionTweener?.TriggerPositionByDuration(endPosition, 3.45f);
    }

    IEnumerator StartEscape()
    {
        Transform transform = this.transform;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x - 3f, startPosition.y, startPosition.z);
        yield return new WaitForSeconds(1f);
        
        heroProfileAnimator.SetBool("ESCAPE", true);
        positionTweener?.TriggerPositionByDuration(endPosition, 1.5f);

        yield return new WaitForSeconds(.1f);

        this.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
    }

    public void TriggerEscape()
    {
        StartCoroutine(StartEscape());
    }
    public void AnnounceCombo()
    {
        UpdateCombo();
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
