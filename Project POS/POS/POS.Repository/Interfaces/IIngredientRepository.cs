﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Entities;

namespace POS.Repository.Interfaces
{
    public interface IIngredientRepository : IDisposable
    {
        IEnumerable<Ingredient> GetAllIngredients();
        Ingredient GetIngredientById(int ingredientId);
        void InsertIngredient(Ingredient ingredient);
        void DeleteIngredient(int ingredientId);
        void UpdateIngredient(Ingredient ingredient);
        void Save();
    }
}
