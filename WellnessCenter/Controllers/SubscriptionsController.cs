using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WellnessCenter.Data;
using WellnessCenter.Models;

namespace WellnessCenter.Controllers;

[Authorize]
public class SubscriptionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SubscriptionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Subscriptions
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Subscription
                    .Include(s => s.Customer)
                    .Include(s => s.Employee)
                    .Include(s => s.Service);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Subscriptions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Subscription == null)
        {
            return NotFound();
        }

        var subscription = await _context.Subscription
            .Include(s => s.Customer)
            .Include(s => s.Employee)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subscription == null)
        {
            return NotFound();
        }

        return View(subscription);
    }

    // GET: Subscriptions/Create
    public IActionResult Create()
    {
        ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id");
        ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id");
        return View();
    }

    // POST: Subscriptions/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PaymentAmount,PaymentStatus,ServiceId,SubscriptionDate,CustomerId")] SubscriptionDto subscription)
    {
        var sub = new Subscription()
        {
            PaymentAmount = subscription.PaymentAmount,
            PaymentStatus = subscription.PaymentStatus,
            ServiceId = subscription.ServiceId,
            SubscriptionDate = subscription.SubscriptionDate,
            CustomerId = subscription.CustomerId,
            EmployeeUserName = User.Identity!.Name!
        };

        if (ModelState.IsValid)
        {
            _context.Add(sub);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", sub.CustomerId);
        ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", sub.ServiceId);
        return View(sub);
    }

    // GET: Subscriptions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Subscription == null)
        {
            return NotFound();
        }

        var subscription = await _context.Subscription.FindAsync(id);
        if (subscription == null)
        {
            return NotFound();
        }
        ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", subscription.CustomerId);
        ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", subscription.ServiceId);
        return View(subscription);
    }

    // POST: Subscriptions/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentAmount,PaymentStatus,ServiceId,SubscriptionDate,CustomerId")] Subscription subscription)
    {
        if (id != subscription.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subscription);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(subscription.Id))
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
        ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Id", subscription.CustomerId);
        ViewData["ServiceId"] = new SelectList(_context.Service, "Id", "Id", subscription.ServiceId);
        return View(subscription);
    }

    // GET: Subscriptions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Subscription == null)
        {
            return NotFound();
        }

        var subscription = await _context.Subscription
            .Include(s => s.Customer)
            .Include(s => s.Employee)
            .Include(s => s.Service)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subscription == null)
        {
            return NotFound();
        }

        return View(subscription);
    }

    // POST: Subscriptions/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Subscription == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Subscription'  is null.");
        }
        var subscription = await _context.Subscription.FindAsync(id);
        if (subscription != null)
        {
            _context.Subscription.Remove(subscription);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubscriptionExists(int id)
    {
        return (_context.Subscription?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
