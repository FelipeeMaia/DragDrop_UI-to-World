using UnityEngine;
using UnityEngine.UI;

public class GearColor : MonoBehaviour
{
    private Image image;
    private SpriteRenderer spriteRenderer;

    private Color color;
    public Color Color
    {
        get { return color; }
        set
        {
            //se mudar o valor da variavel, muda tbm a cor do objeto
            color = value;
            
            if(image != null)
                image.color = color;
            else
                spriteRenderer.color = color;
        }
    }

    void Awake()
    {
        //salva a cor do objeto no awake
        try
        {
            image = GetComponent<Image>();
            color = image.color;
        }
        catch
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            color = spriteRenderer.color;
        }        
    }
}
