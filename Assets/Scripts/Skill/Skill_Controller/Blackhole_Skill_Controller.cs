using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    public bool playerCanExitState;

    private float maxSize;
    private float growSpeed;
    private bool canGrow = true;

    private float shrinkSpeed;
    private bool canShrink;

    private List<Transform> enemyTargets = new List<Transform>();
    public List<KeyCode> keyCodeList;

    private float blackholeTimer;

    private bool playerCanDisappear = true;
    private bool enemyDetected;
    private bool canCreateHotkey = true;
    private bool cloneAttackReleased;
    private int attackAmounts = 4;
    private float attackCooldown = .3f;
    private float attackTimer;

    private List<GameObject> createdHotKeys = new List<GameObject>();

    public void SetupBlackhole(float maxSize, float growSpeed, float shrinkSpeed, int attackAmount, float attackCooldown, float blackholeDuration)
    {
        this.maxSize = maxSize;
        this.growSpeed = growSpeed;
        this.shrinkSpeed = shrinkSpeed;
        this.attackAmounts = attackAmount;
        this.attackCooldown = attackCooldown;
        blackholeTimer = blackholeDuration;
    }


    void Update()
    {

        blackholeTimer -= Time.deltaTime;

        if (blackholeTimer < 0)
        {
            blackholeTimer = Mathf.Infinity;

            if (enemyTargets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlacholeAbility();
        }

        

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }    

        if (!canGrow && canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1,-1), shrinkSpeed * Time.deltaTime);

            if(transform.localScale.x < 1)
            {
                Destroy(gameObject);
            }
        }

        attackTimer -= Time.deltaTime;

        if(enemyTargets.Count > 0 )
        {
            enemyDetected = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && enemyDetected)
        {
            ReleaseCloneAttack();
        }

        if (attackTimer < 0 && cloneAttackReleased && attackAmounts > 0)
        {
            attackTimer = attackCooldown;

            float xOffset;

            if(Random.Range(0,100) > 50)
                xOffset = 1;
            else 
                xOffset = -1;

            int randomIndex = Random.Range(0, enemyTargets.Count);
            SkillManager.instance.clone.CreateClone(enemyTargets[randomIndex], 2, new Vector3(xOffset, 0));
            attackAmounts--;

            if (attackAmounts <= 0)
            {
                FinishBlacholeAbility();
            }
        }


    }

    private void FinishBlacholeAbility()
    {
        DestroyHotKeys();
        playerCanExitState = true;
        canGrow = false;
        canShrink = true;
        enemyDetected = false;
        cloneAttackReleased = false;
    }

    private void ReleaseCloneAttack()
    {
        if (enemyTargets.Count < 0)
            return;

        DestroyHotKeys();
        canCreateHotkey = false;
        cloneAttackReleased = true;

        if (playerCanDisappear)
        {
            playerCanDisappear = false;
            PlayerManager.instance.player.MakeTransparent(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);

        }
    }

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTime(false);
        
   


    private void DestroyHotKeys()
    {
        if (createdHotKeys.Count <= 0)
            return;

        for(int i = 0; i < createdHotKeys.Count; i++)
        {
            Destroy(createdHotKeys[i]);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {

        if(keyCodeList.Count <= 0)
        {
            Debug.LogWarning("No Keycode found");
            return;
        }

        if (!canCreateHotkey)
            return;
        GameObject hotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKeys.Add(hotKey);
        KeyCode chosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(chosenKey);

        Blackhole_Hotkey_Controller hotKey_controller = hotKey.GetComponent<Blackhole_Hotkey_Controller>();
        hotKey_controller.SetupHotkey(chosenKey, collision.transform, this);
    }

    public void AddEnemyToTarget(Transform enemy) => enemyTargets.Add(enemy);
}
