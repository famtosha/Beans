using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public TextMeshPro text;

    public void Draw(string text, float duration)
    {
        this.text.text = text;
        Destroy(gameObject, duration);
    }
}
