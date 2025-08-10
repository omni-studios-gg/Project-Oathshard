using UnityEngine;

namespace Omni
{
    public class HelloWorld : MonoBehaviour
    {
        
        public interface IDamageable
        {
            void TakeDamage(int amount);
            bool IsDead { get; }
        }
        
        
        
        public enum  GameState
        {
            Starting,
            Playing,
            Paused,
            GameOver,
        }
        
        
        
        
        public void SayHello()
        {
            Debug.Log("Hello World from DLL!");
        }
        
    }
}