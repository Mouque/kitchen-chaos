using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        containerCounter.OnPlayerGrabbedObjectCarryingPlate += ContainerCounter_OnPlayerGrabbedObjectCarryingPlate;

    }

    private void ContainerCounter_OnPlayerGrabbedObjectCarryingPlate(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
