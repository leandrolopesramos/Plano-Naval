using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffSet : MonoBehaviour
{

    private Material materialAtual;
    private float offset;
    public float velocidade;



    // Start is called before the first frame update
    void Start()
    {
        materialAtual = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        offset += 0.01f;
        velocidade = 0.1f;

        materialAtual.SetTextureOffset("_MainTex", new Vector2(offset * velocidade, 0));

    }
}
