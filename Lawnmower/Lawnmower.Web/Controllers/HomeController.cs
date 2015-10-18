using Lawnmower.BusinessLogic;
using Lawnmower.Dto;
using Lawnmower.Interfaces;
using Lawnmower.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Lawnmower.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILawnmowerCommander lawnmowerCommander;
        private readonly IInstructionsGenerator instructionsGenerator;
        
        public HomeController()
        {
            lawnmowerCommander = new LawnmowerCommander();
            instructionsGenerator = new InstructionsGenerator();
        }

        public HomeController(InstructionsGenerator instructionsGenerator, ILawnmowerCommander lawnmowerCommander)
        {
            this.instructionsGenerator = instructionsGenerator;
            this.lawnmowerCommander = lawnmowerCommander;
        }

        [Route("/")]
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [Route("/")]
        [HttpPost]
        public ActionResult Index(HomeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            IList<LawnmowerInstructions> instructions;

            try
            {
                instructions = instructionsGenerator.GetInstructions(viewModel.LawnmowersInstructions);
            }catch(ArgumentException)
            {
                ModelState.AddModelError("Error", "Invalid Command");
                return View(viewModel);
            }


            foreach(var instruction in instructions)
            {
                viewModel.LawnmowerPositions.Add(lawnmowerCommander.RunInstructions(instruction));
            }

            return View(viewModel);
        }
    }
}