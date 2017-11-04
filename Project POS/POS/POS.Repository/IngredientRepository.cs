using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Context;
using POS.Entities;
using POS.Repository.Interfaces;

namespace POS.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private AsowellContext context;

        public IngredientRepository(AsowellContext context)
        {
            this.context = context;
        }

        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return context.Ingredients.ToList();
        }

        public Ingredient GetIngredientById(int ingredientId)
        {
            return context.Ingredients.Find(ingredientId);
        }

        public void InsertIngredient(Ingredient ingredient)
        {
            context.Ingredients.Add(ingredient);
        }

        public void DeleteIngredient(int ingredientId)
        {
            Ingredient ingredient = context.Ingredients.Find(ingredientId);
            context.Ingredients.Remove(ingredient);
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            context.Entry(ingredient).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
