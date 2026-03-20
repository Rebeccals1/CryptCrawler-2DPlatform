using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] float floatSpeed = 1.5f;
    [SerializeField] float lifetime = 0.8f;

    TextMeshProUGUI text;
    Color startColor;
    float timer;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        startColor = text.color;
    }

    public void SetValue(int amount)
    {
        text.text = amount.ToString();
        Debug.Log("DamageNumber spawned at: " + transform.position + " value: " + amount);
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        text.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1f, 0f, timer / lifetime));
        if (timer >= lifetime)
            Destroy(gameObject);
    }
}