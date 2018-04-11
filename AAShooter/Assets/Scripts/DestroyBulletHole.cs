using UnityEngine;
using System.Collections;

public class DestroyBulletHole : MonoBehaviour
{
    private float interValue = 0.0f;
    public float timeToInterpolate = 3.0f;
    private Renderer m_renderer;
    private float alpha;


    // Use this for initialization
    void Start()
    {
        m_renderer = GetComponentInChildren<Renderer>();
        Invoke("StartCoRo", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartCoRo()
    {
        StartCoroutine("FadeOut");
    }


    private IEnumerator FadeOut()
    {
        float timer = 0;
        interValue = 0;

        while (interValue <= 1)
        {
            timer += Time.deltaTime;

            interValue = timer / timeToInterpolate;

            alpha = Mathf.Lerp(1.0f, 0.0f, interValue);            

            Color c = m_renderer.material.GetColor("_Color");
            c.a = alpha;

            m_renderer.material.SetColor("_Color", c);

            yield return null;
        }
        Destroy(gameObject);
    }
}
