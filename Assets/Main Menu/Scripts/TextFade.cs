using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public float FadeSpeed = 1f, MinAlpha = 0.5f;
    private Text TextComponent;
    
    void Start()
    {
        TextComponent = GetComponent<Text>();
    }
    void Update()
    {
        TextComponent.color = new Color(TextComponent.color.r, TextComponent.color.g, TextComponent.color.b, Mathf.Cos(Time.time * FadeSpeed) + MinAlpha);
    }
}
