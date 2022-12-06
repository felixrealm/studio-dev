using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlendShadows : MonoBehaviour
{
    private FieldOfView DissolveCheck; 

    [HideInInspector]
    public float DissolveAmount = 0;

    private float DissolveSpeed = 0.5f;
    public Vector3 scaleChange;

    [HideInInspector]
    public Renderer rend;

    public Animator anim;

    [SerializeField] public Destroy parentDestroy;

    [SerializeField] public Image healthBar;

    public AudioSource ghostDeath;

    void Start()
    {
        
        DissolveAmount = 0;
        anim = GetComponent<Animator>();


    }
    void Update()
    {
        UpdateHealthBar();
        rend.material.SetFloat("_DissolveAmount", DissolveAmount);
    }
    void Awake()
    {
        rend = gameObject.GetComponent<Renderer> ();

        scaleChange = new Vector3(0.0001f, 0.0001f, 0.0001f);

    }
    public void reset()
    {
        if(DissolveAmount < 100)
        {
            DissolveAmount += DissolveSpeed;
        }
        
    }
    public void scaleIncrease()
    {
        if(DissolveAmount > 0)
        {
            DissolveAmount -= DissolveSpeed;
            if(DissolveAmount == 0)
            {
                DissolveAmount = 0;

            }
        }
    }

    public void DestroyGhost()
    {
        parentDestroy.DestroyParent();
    }
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = DissolveAmount / 100;
    }
    public void PlayDeathSound()
    {
        ghostDeath.Play();
    }
}
