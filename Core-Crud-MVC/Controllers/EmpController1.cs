using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Crud_MVC.Models;
using Microsoft.EntityFrameworkCore;


namespace Core_Crud_MVC.Controllers
{
    public class EmpController1 : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpController1(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var displaydata = _db.EmployeeTable.ToList();
            return View(displaydata);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewEmpClass nec)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Add(nec);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            return View(nec);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewEmpClass nc)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _db.Update(nc);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }

            }
            return View(nc);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            return View(getempdetails);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var getempdetails = await _db.EmployeeTable.FindAsync(id);
            try
            {
                _db.EmployeeTable.Remove(getempdetails);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }

        }
    }
}
