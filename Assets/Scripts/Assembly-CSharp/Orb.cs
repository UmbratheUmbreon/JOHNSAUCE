using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Orb : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        source = base.GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerOrb)
            {
                base.StartCoroutine(PowerOrb());
            }
            source.PlayOneShot(collect);
            orbsCollected++;
            text.text = orbsCollected + "/21";
            if (orbsCollected % 3 == 0)
            {
                SpawnNew();
            }
            if (orbsCollected == 20)
            {
                finalOrb.position = new Vector3(-10f, 3f, 0f);
            }
            if (orbsCollected == 21)
            {
                BeatGame();
            }
            base.gameObject.GetComponent<MeshRenderer>().enabled = false;
            base.gameObject.GetComponent<Light>().enabled = false;
            base.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(base.gameObject, 10f);
        }
    }

    private void SpawnNew()
    {
        GameObject obj = Instantiate(newObjecr, new Vector3(-10f, 1f, 0f), Quaternion.identity);
        AgentTest test = obj.GetComponent<AgentTest>();
        test.player = agent.player;
        test.wanderTarget = agent.wanderTarget;
        test.wanderer = agent.wanderer;
    }

    private void BeatGame()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        winScreen.SetActive(true);
        winSource.ignoreListenerPause = true;
        winSource.Play();
        base.StartCoroutine(BeatGame2());
    }

    private IEnumerator BeatGame2()
    {
        yield return new WaitForSecondsRealtime(winSource.clip.length);
        Application.Quit();
        yield break;
    }

    private IEnumerator PowerOrb()
    {
        player.walkSpeed *= 1.25f;
        yield return new WaitForSeconds(20f);
        player.walkSpeed /= 1.25f;
        yield break;
    }

    [SerializeField]
    private Transform finalOrb;

    [SerializeField]
    private PlayerScript player;

    [SerializeField]
    private AgentTest agent;

    [SerializeField]
    private GameObject newObjecr;

    [SerializeField]
    private bool powerOrb = false;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private AudioClip collect;
    
    [SerializeField]
    private AudioSource winSource;

    private AudioSource source;

    [SerializeField]
    private TMP_Text text;

    private static int orbsCollected = 0;
}