using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class Sword_Skill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float returnSpeed;

    Vector2 finalDir;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private Transform dotParent;
    private GameObject[] dots;

    [Header("Bounce Info")]
    [SerializeField] private int bounceAmount = 5;
    [SerializeField] private float bounceGravity = 5;
    private float bounceSpeed = 20;

    [Header("Pierce Info")]
    [SerializeField] private int pierceAmount = 2;
    [SerializeField] private float pierceGravity;

    [Header("Spin Info")]
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    private float spinGravity = 1;

    protected override void Start()
    {
        base.Start();

        GenerateDots();
        SetupGravity();
    }

    private void SetupGravity()
    {
        if (swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if(swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if(swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    protected override void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse2))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }


        if (Input.GetKey(KeyCode.Mouse2))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public void CreateSword()
    {
        GameObject sword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        Sword_Skill_Controller swordController = sword.GetComponent<Sword_Skill_Controller>();

        if (swordType == SwordType.Bounce)
            swordController.SetupBounce(true, bounceAmount, bounceSpeed);
        else if (swordType == SwordType.Pierce)
            swordController.SetupPierce(pierceAmount);
        else if (swordType == SwordType.Spin)
            swordController.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);

        swordController.SetupSword(finalDir, swordGravity, player, returnSpeed);
        player.AssignSword(sword);
        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        return direction;
    }

    public void DotsActive(bool IsActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(IsActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y
            ) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }
}
