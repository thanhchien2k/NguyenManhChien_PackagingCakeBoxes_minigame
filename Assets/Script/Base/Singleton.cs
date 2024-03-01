using UnityEngine;

namespace BaseGame.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        SetUpInstance();
                    }
                    else
                    {
                        string typeName = typeof(T).Name;
                        Debug.Log("Singleton of " + typeName + " instance already created: " + instance.gameObject.name);
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            RemoveDupalicates();
        }

        private static void SetUpInstance()
        {
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject newObj = new();
                newObj.name = typeof(T).Name;
                instance = newObj.AddComponent<T>();

                DontDestroyOnLoad(newObj);
            }
        }

        private void RemoveDupalicates()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
