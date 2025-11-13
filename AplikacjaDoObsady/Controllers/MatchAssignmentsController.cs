using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplikacjaDoObsady.Data;
using AplikacjaDoObsady.Models;

namespace AplikacjaDoObsady.Controllers
{
    public class MatchAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MatchAssignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MatchAssignment.Include(m => m.AssistantReferee1).Include(m => m.AssistantReferee2).Include(m => m.FourthOfficial).Include(m => m.MainReferee).Include(m => m.Match);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MatchAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAssignment = await _context.MatchAssignment
                .Include(m => m.AssistantReferee1)
                .Include(m => m.AssistantReferee2)
                .Include(m => m.FourthOfficial)
                .Include(m => m.MainReferee)
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.MatchAssignmentId == id);
            if (matchAssignment == null)
            {
                return NotFound();
            }

            return View(matchAssignment);
        }

        // GET: MatchAssignments/Create
        public IActionResult Create()
        {
            ViewData["AssistantReferee1Id"] = new SelectList(_context.Referee, "RefereeId", "FullName");
            ViewData["AssistantReferee2Id"] = new SelectList(_context.Referee, "RefereeId", "FullName");
            ViewData["FourthOfficialId"] = new SelectList(_context.Referee, "RefereeId", "FullName");
            ViewData["MainRefereeId"] = new SelectList(_context.Referee, "RefereeId", "FullName");
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchTitle");
            return View();
        }

        // POST: MatchAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchAssignmentId,MatchId,MainRefereeId,AssistantReferee1Id,AssistantReferee2Id,FourthOfficialId,Status")] MatchAssignment matchAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssistantReferee1Id"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.AssistantReferee1Id);
            ViewData["AssistantReferee2Id"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.AssistantReferee2Id);
            ViewData["FourthOfficialId"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.FourthOfficialId);
            ViewData["MainRefereeId"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.MainRefereeId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", matchAssignment.MatchId);
            return View(matchAssignment);
        }

        // GET: MatchAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAssignment = await _context.MatchAssignment.FindAsync(id);
            if (matchAssignment == null)
            {
                return NotFound();
            }
            ViewData["AssistantReferee1Id"] = new SelectList(_context.Referee, "RefereeId", "FullName", matchAssignment.AssistantReferee1Id);
            ViewData["AssistantReferee2Id"] = new SelectList(_context.Referee, "RefereeId", "FullName", matchAssignment.AssistantReferee2Id);
            ViewData["FourthOfficialId"] = new SelectList(_context.Referee, "RefereeId", "FullName", matchAssignment.FourthOfficialId);
            ViewData["MainRefereeId"] = new SelectList(_context.Referee, "RefereeId", "FullName", matchAssignment.MainRefereeId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchTitle", matchAssignment.MatchId);
            return View(matchAssignment);
        }

        // POST: MatchAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchAssignmentId,MatchId,MainRefereeId,AssistantReferee1Id,AssistantReferee2Id,FourthOfficialId,Status")] MatchAssignment matchAssignment)
        {
            if (id != matchAssignment.MatchAssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchAssignmentExists(matchAssignment.MatchAssignmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssistantReferee1Id"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.AssistantReferee1Id);
            ViewData["AssistantReferee2Id"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.AssistantReferee2Id);
            ViewData["FourthOfficialId"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.FourthOfficialId);
            ViewData["MainRefereeId"] = new SelectList(_context.Referee, "RefereeId", "RefereeId", matchAssignment.MainRefereeId);
            ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId", matchAssignment.MatchId);
            return View(matchAssignment);
        }

        // GET: MatchAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchAssignment = await _context.MatchAssignment
                .Include(m => m.AssistantReferee1)
                .Include(m => m.AssistantReferee2)
                .Include(m => m.FourthOfficial)
                .Include(m => m.MainReferee)
                .Include(m => m.Match)
                .FirstOrDefaultAsync(m => m.MatchAssignmentId == id);
            if (matchAssignment == null)
            {
                return NotFound();
            }

            return View(matchAssignment);
        }

        // POST: MatchAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchAssignment = await _context.MatchAssignment.FindAsync(id);
            if (matchAssignment != null)
            {
                _context.MatchAssignment.Remove(matchAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchAssignmentExists(int id)
        {
            return _context.MatchAssignment.Any(e => e.MatchAssignmentId == id);
        }
    }
}
