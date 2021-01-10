﻿using UnityEngine;
using Pathfinding;

public class LifeGuardian : ObjectSpawner
{
    [Space(10)]
    [Range(1, 100), SerializeField] int plantingArea = 2;
    [Range(1, 100)] public int roamingArea = 30;
    [Range(1, 50)] public int pathingSpread = 5;


    Animator guardianBrain;
    AIPath pathing;
    GlobalLifeSource lifeSource;

    void Start()
    {
        guardianBrain = GetComponent<Animator>();
        pathing = transform.root.GetComponent<AIPathAlignedToSurface>();
        lifeSource = Servius.Server.GetComponent<GlobalLifeSource>();
    }

    void OnEnable()
    {
        PlayerSoul.Cam.lifeGuardian = transform.root.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
    }

    void OnDisable()
    {
        if (Servius.Server != null)
        {
            PlayerSoul.Cam.lifeGuardian = null;

            //Spawn new Guardian if destroyed
            //lifeSource.SpawnMeteor();
        }
    }

    void Update()
    {
        guardianBrain.SetFloat("DestinationDistance", pathing.remainingDistance);
    }


    //Plant Seedgrass
    public void PlantSeedFromSource()
    {
        lifeSource.PlantSeedFromSource(UtilityFunctions.FindNearbyPos(transform.root, plantingArea, true));
    }
}
