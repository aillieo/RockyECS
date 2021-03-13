using AillieoUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils.TypeExt;
using UnityEngine;

namespace RockyECS
{
    public class Engine
    {
        public readonly Event<float> onTimeScaleChanged = new Event<float>();

        public bool isPlaying = true;

        private float timer;
        private int frame;
        private float frameTimer;
        private float mTimeScale = 1;

        private readonly List<BaseSystem> systems = new List<BaseSystem>();

        private readonly List<IFilteredUpdatingSystem> updatingSystems = new List<IFilteredUpdatingSystem>();

        private readonly Dictionary<IFilteredUpdatingSystem, Selection[]> cachedSelections = new Dictionary<IFilteredUpdatingSystem, Selection[]>();
        private readonly Context context = new Context();

        public Engine()
        {
            Scheduler.ScheduleUpdate(Update);
        }

        public Engine AddSystem<T>() where T : BaseSystem
        {
            T sys = Activator.CreateInstance<T>();
            return AddSystem(sys);
        }

        private Engine AddSystem(BaseSystem system)
        {
            system.context = context;

            if(system is ISystemBootstrap bootstrap)
            {
                bootstrap.InitContext();
            }

            if(system is ICompositeSystem composite)
            {
                foreach(var sub in composite.GetSystems())
                {
                    AddSystem(sub);
                }

                return this;
            }
            else
            {
                systems.Add(system);
            }

            if(system is IFilteredUpdatingSystem ifus)
            {
                Selection[] selections = ifus.CreateFilters().Select(f => context.CreateSelection(f)).ToArray();
                cachedSelections.Add(ifus, selections);
                updatingSystems.Add(ifus);
            }

            return this;
        }

        public float timeScale
        {
            get
            {
                return mTimeScale;
            }
            set
            {
                if (mTimeScale != value)
                {
                    mTimeScale = value;
                    onTimeScaleChanged.Invoke(mTimeScale);
                }
            }
        }

        private void Update()
        {
            if(!isPlaying)
            {
                return;
            }

            timer += UnityEngine.Time.deltaTime * timeScale;
            float timeStep = 0.01f;
            while (timer > timeStep)
            {
                timer -= timeStep;

                foreach (var s in updatingSystems)
                {
                    Selection[] ss = cachedSelections[s];
                    for (int i = 0; i < ss.Length; ++i)
                    {
                        Selection sel = ss[i];
                        if(sel.Any())
                        {
                            s.Update(i, sel, timeStep);
                        }
                    }
                }
            }
        }
    }
}
