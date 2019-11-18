﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MazeObjects
{
    class BuzzSaws : Feature
    {
        public Vector3 Origin { get; private set; }
        public GameObject MazeObject { get; private set; }
        public GameObject BuzzSawPrefab { get; private set; }
        private List<BuzzSaw> buzzSaws;
        public Maze maze;
        private float counterMax;
        private float counter;
        private float buzzSawTimer;

        public BuzzSaws(Maze maze, bool[,] uniqueObjects) : base(uniqueObjects)
        {
            buzzSaws = new List<BuzzSaw>();
            this.maze = maze;
            Origin = maze.Origin;
            MazeObject = maze.gameObject;
            BuzzSawPrefab = (GameObject)Resources.Load("Prefabs/BuzzSaw");
            this.Initialize();
        }

        // Use this for initialization
        public void Initialize()
        {
            //Time between spawns
            counterMax = 1 - (Director.Difficulty) / 100;
            counter = 0;
            //Time for a ball to despawn
            buzzSawTimer = 2 + Director.Difficulty / 100;
        }

        public override void Build()
        {
            /*foreach (FeatureObject buzzSaw in buzzSaws)
            {
                buzzSaw.Build();
            }*/
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            if (counter >= counterMax)
            {
                //Chance of spawning
                if (Random.value < 0.25)
                {
                    BuzzSaw buzzSaw = new BuzzSaw(Random.Range(0, (int)(this.maze.Width)), Random.Range(0, (int)this.maze.Height), BuzzSawPrefab, buzzSawTimer, Origin);
                    buzzSaws.Add(buzzSaw);
                }
                counter = 0;
            }
            for (int i = buzzSaws.Count - 1; i >= 0; --i)
            {
                if (buzzSaws[i].Dead)
                {
                    buzzSaws.RemoveAt(i);
                    continue;
                }
                buzzSaws[i].Update();
            }
            /*foreach (FeatureObject buzzSaw in buzzSaws)
            {
                buzzSaw.Update();
            }*/
        }
    }
}