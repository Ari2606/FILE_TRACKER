using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rec_Tracker.Models;
using Rec_Tracker.Data;

namespace Rec_Tracker.Controllers
{
    public class RecDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public RecDetailsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var records = _context.Record_details.AsEnumerable(); // fetch all first

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                records = records.Where(r =>
                    (r.File_Name != null && r.File_Name.ToLower().Contains(searchString)) ||
                    (r.Organisation != null && r.Organisation.ToLower().Contains(searchString)) ||
                    (r.File_Type != null && r.File_Type.ToLower().Contains(searchString)) ||
                    (r.File_Description != null && r.File_Description.ToLower().Contains(searchString)) ||
                    r.Shelf_Number.ToString().Contains(searchString) ||
                    r.Row_number.ToString().Contains(searchString) ||
                    r.Position.ToString().Contains(searchString) ||
                    r.File_Number.ToString().Contains(searchString)
                );
            }

            return View(records);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("File_Number,File_Name,Organisation,File_Type,File_Description,Shelf_Number,Row_number,Position")] Rec_Details rec)
        {
            if (ModelState.IsValid)
            {
                bool duplicateLocation = await _context.Record_details.AnyAsync(r =>
                    r.Shelf_Number == rec.Shelf_Number &&
                    r.Row_number == rec.Row_number &&
                    r.Position == rec.Position);

                if (duplicateLocation)
                {
                    ModelState.AddModelError("", "Another file already exists in the same Shelf, Row, and Position.");
                    TempData["ErrorMessage"] = "Duplicate location detected.";
                    return View(rec);
                }

                try
                {
                    _context.Add(rec);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "File created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("File_Number", "A file with this number already exists.");
                }
            }

            TempData["ErrorMessage"] = "Failed to create file. Please check the inputs.";
            return View(rec);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rec = await _context.Record_details.FirstOrDefaultAsync(r => r.File_Number == id);
            if (rec == null) return NotFound();

            return View(rec);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("File_Number,File_Name,Organisation,File_Type,File_Description,Shelf_Number,Row_number,Position")] Rec_Details rec)
        {
            if (id != rec.File_Number) return NotFound();

            if (ModelState.IsValid)
            {
                bool duplicateLocation = await _context.Record_details.AnyAsync(r =>
                    r.Shelf_Number == rec.Shelf_Number &&
                    r.Row_number == rec.Row_number &&
                    r.Position == rec.Position &&
                    r.File_Number != rec.File_Number); // ignore current file

                if (duplicateLocation)
                {
                    ModelState.AddModelError("", "Another file already exists in the same Shelf, Row, and Position.");
                    TempData["ErrorMessage"] = "Duplicate location detected.";
                    return View(rec);
                }

                try
                {
                    _context.Update(rec);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "File updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Rec_DetailsExists(rec.File_Number))
                        return NotFound();
                    else
                        throw;
                }
            }

            TempData["ErrorMessage"] = "Failed to update file. Please check the inputs.";
            return View(rec);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rec = await _context.Record_details
                .FirstOrDefaultAsync(m => m.File_Number == id);

            if (rec == null) return NotFound();

            return View(rec);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rec = await _context.Record_details.FindAsync(id);
            if (rec != null)
            {
                _context.Record_details.Remove(rec);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "File deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "File not found.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool Rec_DetailsExists(int id)
        {
            return _context.Record_details.Any(e => e.File_Number == id);
        }
    }
}
