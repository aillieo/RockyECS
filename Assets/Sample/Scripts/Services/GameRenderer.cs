using AillieoUtils;

namespace Sample
{
    public class GameRenderer : SingletonMonoBehaviour<GameRenderer>
    {
        public Event<int> renderEvent = new Event<int>();

        private void Update()
        {
            this.renderEvent?.Invoke(0);
        }
    }
}
