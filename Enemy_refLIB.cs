using UnityEngine;

public class Enemy_refLIB : MonoBehaviour
{
    // Health and Damage Settings
    [Header("Health and Damage Settings")]
    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public int armorClass = 0;
    public int attackDamage = 10;

    // Movement Settings
    [Header("Movement Settings")]
    public float movementSpeed = 2.0f;

    // Enemy Stats
    [Header("Enemy Stats")]
    public int hitPoints = 100;
    public int armor = 6;
    public int pointValue = 2;
    public int moveSpeed = 1;

    // Enemy Audio Controls
    [Header("Enemy Audio Controls")]
    public AudioSource cryAudioSource;
    public AudioClip[] cry_CLIP;
    public float cryMinVol = 0.064f;
    public float cryMaxVol = 0.692f;
    public float cryMinPitch = 0.483f;
    public float cryMaxPitch = 1.241f;
    
    public AudioSource walkAudioSource;
    public AudioClip[] walkClips;
    public float walkMinVol = 0.512f;
    public float walkMaxVol = 1.433f;
    public float walkMinPitch = 0.483f;
    public float walkMaxPitch = 1.669f;

    // Materials for Customization
    [Header("Color Options")]
    public int mats_skin = 0;
    public int mats_clothing = 0;
    public int mats_armor = 0;
    public Color skinToColorize;

    // Boss Variables
    [HideInInspector] public bool isBossAlive = false;

    private void Start()
    {
        // Initialize current HP to max HP at start
        currentHP = maxHP;
    }
}
