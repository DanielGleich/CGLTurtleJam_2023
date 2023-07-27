using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanManager : MonoBehaviour
{
    private SpriteRenderer panRenderer;
    [SerializeField] private Sprite panEmpty;
    [SerializeField] private Sprite panFull;

    private void Awake()
    {
        panRenderer = GetComponent<SpriteRenderer>();
        panRenderer.sprite = panEmpty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            panRenderer.sprite = panFull;
            Destroy(collision.gameObject);
        }
    }
}
