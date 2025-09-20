using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private Renderer Renderer;

    public enum foodType { GlutenFreeBones, BreadB, BreadT, Meat, Cheese, FrenchFry }
    [SerializeField] private foodType type;

    public enum cookLevel { Raw, Cooked, Burnt, Ethereal}
    [SerializeField] private cookLevel cookedLevel;

    [SerializeField] private float timeToCook;
    private float heatingProgress;
    private float etherealTime;

    private Color rawColor;
    private Color cookedColor;
    private Color burntColor;

    private void Start()
    {
        rawColor = Renderer.material.color;
        cookedColor = rawColor - new Color(0.3f, 0.3f, 0.6f, 0.5f);
        burntColor = Color.black;
        heatingProgress = timeToCook;
        etherealTime = timeToCook * 10;
    }

    public virtual void heatUp()
    {
        heatingProgress -= Time.deltaTime;

        if (heatingProgress <= 0 && cookedLevel != cookLevel.Ethereal)
        {
            switch (cookedLevel)
            {
                case cookLevel.Raw:
                    cookedLevel = cookLevel.Cooked;
                    heatingProgress = timeToCook;
                    Renderer.material.color = cookedColor;
                    break;
                case cookLevel.Cooked:
                    cookedLevel = cookLevel.Burnt;
                    heatingProgress = etherealTime;
                    
                    Renderer.material.color = burntColor;
                    break;
                case cookLevel.Burnt:
                    cookedLevel = cookLevel.Ethereal;
                    heatingProgress = timeToCook;
                    break;
                case cookLevel.Ethereal:
                    break;
            }
        }
    }

    public virtual void Delete()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (cookedLevel == cookLevel.Ethereal)
        {
            Renderer.material.color += new Color(1f, 0.3f, 0.1f, 1f);
        }
    }
}
