using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTweens : MonoBehaviour
{
    [SerializeField] private GameObject program;
    [SerializeField] private GameObject laptop;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject Setting1;
    [SerializeField] private GameObject Setting2;

    private RectTransform LaptopRect;

    private Vector3 programPos;

    private void Awake() {
        program.GetComponent<RectTransform>().localScale = Vector3.zero;
        laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
        settings.GetComponent<RectTransform>().localScale = Vector3.zero;
        LaptopRect = laptop.GetComponent<RectTransform>();
        programPos = program.GetComponent<RectTransform>().position;
    }
    void OnEnable()
    {
        LeanTween.scale(program, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(laptop,new Vector3(2f, 2f, 2f), 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutBack);

        LeanTween.move(program, new Vector3(LaptopRect.position.x, LaptopRect.position.y, LaptopRect.position.z), 1f).setDelay(3f);
        LeanTween.scale(program, Vector3.zero, 1f).setDelay(3f);

        LeanTween.scale(settings, Vector3.one, 1f).setDelay(4f);
        LeanTween.rotateAround(Setting1, Vector3.forward, -360, 5f).setDelay(5f).setLoopClamp();
        LeanTween.rotateAround(Setting2, Vector3.forward, 360, 5f).setDelay(5f).setLoopClamp();
        

    }
    void OnDisable()
    {
        LeanTween.reset();
        program.GetComponent<RectTransform>().localScale = Vector3.zero;
        laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
        settings.GetComponent<RectTransform>().localScale = Vector3.zero;
        LaptopRect = laptop.GetComponent<RectTransform>();
        program.GetComponent<RectTransform>().position = programPos;
    }
}
