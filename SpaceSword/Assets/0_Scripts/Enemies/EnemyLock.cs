using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyLock : MonoBehaviour
{
    public float m_SideSpeed, m_SideLimit, m_ShootRate;
    private bool m_Side;
    public GameObject m_Bullet;
    private Transform m_Player;
    private GameObject m_Turret;
    public LineRenderer m_Laser;

    public AudioClip m_LockShotSFX;
    private AudioSource m_AudioSource;
    void Start()
    {
        m_Player = GameObject.FindWithTag("Player").transform;
        m_Turret = transform.Find("ShootPoint").gameObject;

        m_Laser.positionCount = 2;

        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_LockShotSFX;
    }
    void Update()
    {
        m_Laser.SetPosition(0, transform.Find("ShootPoint").transform.Find("ShootPoint").transform.position);
        m_Laser.SetPosition(1, m_Player.position);

        m_Turret.transform.LookAt(m_Player);

        if (m_Side)
        {
            transform.Translate(Vector3.left * m_SideSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * m_SideSpeed * Time.deltaTime);
        }

        if (transform.position.x >= m_SideLimit)
        {
            m_Side = true;
        }
        if (transform.position.x <= -m_SideLimit)
        {
            m_Side = false;
        }
    }
    void Shoot()
    {
        Instantiate(m_Bullet, transform.position, m_Turret.transform.rotation);
        m_AudioSource.Play();
    }
    void LockActivate()
    {
        m_Turret.transform.Find("ShootPoint").gameObject.SetActive(true);
        GetComponent<Animator>().enabled = false;
        InvokeRepeating("Shoot", 0f, m_ShootRate);
    }
}
