﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private IRepository productRepository;
        private ICategoryRepository categoryRepository;

        public StoreController(IRepository productRepo, ICategoryRepository categoryRepo)
        {
            productRepository = productRepo;
            categoryRepository = categoryRepo;
        }


        public IActionResult Index([FromQuery(Name ="options")]
            QueryOptions productOptions,
            QueryOptions catOptions,
            long category)
        {
            ViewBag.Categories = categoryRepository.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;
            return View(productRepository.GetProducts(productOptions, category));
        }
    }
}