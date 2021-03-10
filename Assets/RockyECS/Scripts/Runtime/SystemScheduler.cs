using AillieoUtils;
using System;
using System.Collections.Generic;

namespace RockyECS
{
    public class SystemScheduler
    {
        public readonly Event<float> onTimeScaleChanged = new Event<float>();

        public bool isPlaying = true;

        private readonly Event<float> evt = new Event<float>();

        private float timer;
        private int frame;
        private float frameTimer;
        private float mTimeScale = 1;

        private readonly List<ISystem> systems = new List<ISystem>();

        private readonly List<IFilteredUpdatingSystem> updatingSystems = new List<IFilteredUpdatingSystem>();
        private readonly List<IFilteredFrameUpdatingSystem> frameUpdatingSystems = new List<IFilteredFrameUpdatingSystem>();

        private readonly Dictionary<ISelectionProvider, Selection> cachedSelections = new Dictionary<ISelectionProvider, Selection>();
        private readonly Container container = Container.Instance; // new Container();

        public SystemScheduler()
        {
            Scheduler.ScheduleUpdate(Update);
        }

        public SystemScheduler AddSystem<T>() where T : ISystem
        {
            T sys = Activator.CreateInstance<T>();
            return AddSystem(sys);
        }

        private SystemScheduler AddSystem(ISystem system)
        {
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

            if(system is ISelectionProvider isp)
            {
                Selection selection = container.CreateSelection(isp.CreateFilter());
                cachedSelections.Add(isp, selection);
            }

            if (system is IFilteredUpdatingSystem iusys)
            {
                updatingSystems.Add(iusys);
            }

            if (system is IFilteredFrameUpdatingSystem ifusys)
            {
                frameUpdatingSystems.Add(ifusys);
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

        public Handle<float> Schedule(Action<float> action)
        {
            return evt.AddListener(action);
        }

        public bool Unschedule(Handle<float> handle)
        {
            return evt.Remove(handle);
        }

        public void UnscheduleAllTasks()
        {
            evt.RemoveAllListeners();
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

                evt.Invoke(timeStep);

                foreach (var s in updatingSystems)
                {
                    s.Update(cachedSelections[s], timeStep);
                }

                int frameCount = UnityEngine.Time.frameCount;
                if (frame != frameCount)
                {
                    frame = frameCount;
                    float frameDeltaTime = timer - frameTimer;
                    frameTimer = timer;

                    foreach (var s in frameUpdatingSystems)
                    {
                        s.FrameUpdate(cachedSelections[s], frameDeltaTime);
                    }
                }

            }
        }
    }

}
