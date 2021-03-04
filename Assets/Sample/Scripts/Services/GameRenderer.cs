using AillieoUtils;

namespace Sample
{
    public class GameRenderer : SingletonMonoBehaviour<GameRenderer>
    {
        public Event renderEvent = new Event();

        private void Update()
        {
            this.renderEvent?.Invoke();
        }
    }
}
