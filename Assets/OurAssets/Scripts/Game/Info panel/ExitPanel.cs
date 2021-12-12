using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    public GameObject infoPanel;
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = exit.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        infoPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            infoPanel.SetActive(true);
        }
    }
}
