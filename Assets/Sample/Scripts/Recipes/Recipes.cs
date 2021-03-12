using System;
using System.Collections.Generic;

namespace Sample
{
    public abstract class Recipes
    {
        private static readonly Dictionary<string, IRecipe> collection = new Dictionary<string, IRecipe>(StringComparer.OrdinalIgnoreCase);

        public static T Get<T>() where T : class, IRecipe
        {
            IRecipe recipe = default;
            string typeName = typeof(T).FullName;
            if (!collection.TryGetValue(typeName, out recipe))
            {
                recipe = Activator.CreateInstance(typeof(T)) as IRecipe;
                collection.Add(typeName, recipe);
            }
            return recipe as T;
        }

        public static IRecipe Get(string recipeName)
        {
            IRecipe recipe = default;
            if (!collection.TryGetValue(recipeName, out recipe))
            {
                Type t = Type.GetType(recipeName);
                if(t == null)
                {
                    throw new Exception($"无效的recipeName {recipeName}");
                }
                recipe = Activator.CreateInstance(t) as IRecipe;
                collection.Add(recipeName, recipe);
            }
            return recipe;
        }
    }
}
