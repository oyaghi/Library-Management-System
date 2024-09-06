using Library_Management_System.Commands;
using Library_Management_System.Data;
using Library_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Members = await _context.Members.ToListAsync();

            return View(Members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberCommand command)
        {
            // Set the Membership of the New Member to only one month 
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            // Checking for Existing Phone number
            var PhoneExists = await _context.Members.SingleOrDefaultAsync(m => m.PhoneNumber.Equals(command.PhoneNumber));
            if (PhoneExists != null)
            {
                ViewData["PhoneExists"] = "Phone number exists in the database";
                return View(command);
            }
            // Checking for Existing Email address
            var EmailExists = await _context.Members.SingleOrDefaultAsync(m => m.Email.Equals(command.Email));
            if (EmailExists != null)
            {
                ViewData["EmailExists"] = "Email address exists in the database";
                return View(command);
            }

            var NewMember = new Member
            {
                FullName = command.FullName,
                PhoneNumber = command.PhoneNumber,
                Email = command.Email
            };

            await _context.Members.AddAsync(NewMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var Member = await _context.Members.SingleOrDefaultAsync(m => m.MemberID.Equals(id));
            if (Member == null)
            {
                return NotFound();
            }

            var UpdateMember = new UpdateMemberCommand
            {
                FullName = Member.FullName,
                PhoneNumber = Member.PhoneNumber,
                Email = Member.Email
            };

            return View(UpdateMember);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateMemberCommand command)
        {
            // Update the Membership Date by 2 weekes  after editing the Member 
            if (!ModelState.IsValid)
            {
                return View(command);

            }

            var Member = await _context.Members.SingleOrDefaultAsync(m => m.MemberID.Equals(id));
            if (Member == null)
            {
                return NotFound();
            }

            if (command.PhoneNumber != Member.PhoneNumber)
            {
                // Checking Existing Phone number 
                var PhoneExists = await _context.Members.SingleOrDefaultAsync(m => m.PhoneNumber.Equals(command.PhoneNumber));
                if (PhoneExists != null)
                {
                    ViewData["PhoneExists"] = "Phone number exists in the database";
                    return View(command);
                }
            }

            if (command.Email != Member.Email)
            {
                // Checking for Existing Email address
                var EmailExists = await _context.Members.SingleOrDefaultAsync(m => m.Email.Equals(command.Email));
                if (EmailExists != null)
                {
                    ViewData["EmailExists"] = "Email address exists in the database";
                    return View(command);
                }
            }

            Member.FullName = command.FullName;
            Member.PhoneNumber = command.PhoneNumber;
            Member.Email = command.Email;

            _context.Update(Member);

            await _context.SaveChangesAsync(); 

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id )
        {
            var Member = await _context.Members.SingleOrDefaultAsync(m=> m.MemberID.Equals(id));
            return View(Member);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            var member = await _context.Members.SingleOrDefaultAsync(m => m.MemberID == id);

            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Redirect to the Index or relevant page
        }


    }
}
