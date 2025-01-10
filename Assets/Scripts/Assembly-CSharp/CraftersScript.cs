using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000CA RID: 202
public class CraftersScript : MonoBehaviour
{
	// Token: 0x060009AE RID: 2478 RVA: 0x000249ED File Offset: 0x00022DED
	private void Start()
	{
		this.agent = base.GetComponent<NavMeshAgent>(); // Defines the nav mesh agent
		this.audioDevice = base.GetComponent<AudioSource>(); //Gets the audio source
	}

	private void Update()
	{
		if (cooldown > 0f)
		{
			cooldown -= Time.deltaTime;
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00024BAC File Offset: 0x00022FAC
	private void FixedUpdate()
	{
		if (cooldown > 0f)
		{
			return;
		}
		Vector3 direction = this.player.position - base.transform.position;
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up * 2f, direction, out raycastHit, float.PositiveInfinity, 769, QueryTriggerInteraction.Ignore) & raycastHit.transform.tag == "Player") // If Arts is Visible, and active and sees the player
		{
			if (!announced)
			{
				announced = true;
				audioDevice.PlayOneShot(aud_Intro);
			}
			agent.SetDestination(player.position);
		}
		else
		{
			spriteImage.sprite = normalSprite;
			announced = false;
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00024C65 File Offset: 0x00023065
	public void GiveLocation(Vector3 location, bool flee)
	{
		if (this.agent.isActiveAndEnabled)
		{
			this.agent.SetDestination(location);
		}
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x00024CBC File Offset: 0x000230BC
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" & announced & cooldown <= 0f) // If arts is angry and is touching the player
		{
			this.cc.enabled = false;
			this.player.position = new Vector3(5f, this.player.position.y, 80f); // Teleport the player to X: 5, their current Y position, Z: 80
			this.baldiAgent.Warp(new Vector3(5f, this.baldi.position.y, 125f)); // Teleport Baldi to X: 5, baldi's Y, Z: 125
			this.player.LookAt(new Vector3(this.baldi.position.x, this.player.position.y, this.baldi.position.z)); // Make the player look at baldi
			this.cc.enabled = true;
			//this.gc.DespawnCrafters(); // Despawn Arts And Crafters
			audioDevice.PlayOneShot(aud_Loop);
			spriteImage.sprite = angrySprite;
			cooldown = 45f;
		}
	}

	// Token: 0x040006A2 RID: 1698
	public Transform player;

	public CharacterController cc;

	// Token: 0x040006A3 RID: 1699
	public Transform playerCamera;

	// Token: 0x040006A4 RID: 1700
	public Transform baldi;

	// Token: 0x040006A5 RID: 1701
	public NavMeshAgent baldiAgent;

	// Token: 0x040006A6 RID: 1702
	public GameObject sprite;

	// Token: 0x040006A7 RID: 1703
	public GameControllerScript gc;

	// Token: 0x040006A8 RID: 1704
	[SerializeField]
	private NavMeshAgent agent;

	// Token: 0x040006A9 RID: 1705
	public Renderer craftersRenderer;

	// Token: 0x040006AA RID: 1706
	public SpriteRenderer spriteImage;

	// Token: 0x040006AB RID: 1707
	public Sprite angrySprite;

	public Sprite normalSprite;

	// Token: 0x040006AC RID: 1708
	private AudioSource audioDevice;

	// Token: 0x040006AD RID: 1709
	public AudioClip aud_Intro;

	// Token: 0x040006AE RID: 1710
	public AudioClip aud_Loop;

	private bool announced = false;

	private float cooldown = 0f;
}
