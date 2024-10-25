using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{


    Image Image;


    // ≈Õƒ° ¿Ã∆Â∆Æ
    Vector2 direction;
    float moveSpeed = 0.01f;
    float SizeSpeed = 1f;
    float ColorSpeed = 1f;


    float MinSize = 0.1f;
    float MaxSize = 0.5f;


    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        float size = Random.Range(MinSize, MaxSize);

        Image.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * SizeSpeed);

        Color color = Image.color;
        color.a = Mathf.Lerp(Image.color.a, 0, Time.deltaTime * ColorSpeed);
        Image.color = color;


        if (Image.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}