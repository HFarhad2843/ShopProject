﻿using Shop.Core.Entities;

namespace Shop.Business.Interfaces;
public interface ICategoryService
{
    void ShowAll(string Role);
    void CreateCategory(Category category);

    void UpdateCategory(Category category);
    void DeleteCategory(int id);
}
