using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneratorBehaviour : MonoBehaviour
{
    public int initialHealth = 200;
    public int currentHealth;
    public GameObject explosion;
    private bool explode = false;
    private AudioSource boomBoom;
    private float expTimer = 0.0f;
    private int countToEnd = 0;

    public Texture2D fadeTexture;
    public float fadeTime = 0.5f;
    private float alpha = 1.0f;
    private float fadeVar = -1.0f;
    private Color texCol;
    public Slider healthSlide;

    void Start()
    {
        currentHealth = initialHealth;
        boomBoom = GetComponent<AudioSource>();
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Generator destroyed");
            explode = true;
        }
    }

    void Update()
    {
        healthSlide.value = currentHealth;
        Debug.Log(healthSlide.value);

        if (explode)
        {
            expTimer += Time.deltaTime;

            if(expTimer >= 0.5f)
            {
                expTimer = 0.0f;
                Instantiate(explosion, transform.position, transform.rotation);
                countToEnd++;

                if(!boomBoom.isPlaying)
                {
                    boomBoom.Play();
                }

                if(countToEnd >= 5)
                {
                    StartCoroutine("EndGame");
                }
            }
        }
    }

    public void OnGUI()
    {
        alpha += fadeVar * fadeTime * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        texCol = GUI.color;
        texCol.a = alpha;
        GUI.color = texCol;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    public void FadingOut()
    {
        fadeVar = 1;
    }

    private IEnumerator EndGame()
    {
        fadeVar = 1;
        yield return new WaitForSeconds(3);
        Application.LoadLevel(2);
    }
}
