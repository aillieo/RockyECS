using RockyECS;
using System;
using System.Collections.Generic;

namespace Sample
{
    public abstract class Recipe
    {
        public abstract void SetupUnit(Entity e);

        private static readonly Dictionary<string, Recipe> collection = new Dictionary<string, Recipe>(StringComparer.OrdinalIgnoreCase);

        public static Recipe Get<T>() where T : Recipe, new()
        {
            Recipe recipe = default;
            string typeName = typeof(T).FullName;
            if (!collection.TryGetValue(typeName, out recipe))
            {
                recipe = new T();
                collection.Add(typeName, recipe);
            }
            return recipe;
        }

        public static Recipe Get(string recipeName)
        {
            Recipe recipe = default;
            if (!collection.TryGetValue(recipeName, out recipe))
            {
                Type t = Type.GetType(recipeName);
                if(t == null)
                {
                    throw new Exception($"无效的recipeName {recipeName}");
                }
                recipe = Activator.CreateInstance(t) as Recipe;
                collection.Add(recipeName, recipe);
            }
            return recipe;
        }
    }
}
