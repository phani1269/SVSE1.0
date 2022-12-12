using ComplaintService.API.DataLayer.Contexts;
using ComplaintService.API.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintService.API.RepositoryLayer
{
    public class ComplaintRepo : IComplaintRepo
    {
        private readonly ComplaintDbContext _context;

        public ComplaintRepo(ComplaintDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ComplaintModel> CreateComplaint(ComplaintModel model)
        {
            var objs = await _context.complaints.AddAsync(model);
            await _context.SaveChangesAsync();
            return objs.Entity;
        }

        public async Task<bool> DeleteComplaint(string userName)
        {
            var con = await _context.complaints.FirstOrDefaultAsync(x => x.UserName == userName);
            if(con == null) return false;
            _context.complaints.Remove(con);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<IEnumerable<ComplaintModel>> GetAllComplaints()
        {
            var obj = await _context.complaints.ToListAsync();
            return obj;
        }

        public async Task<ComplaintModel> GetComplaintByUsername(string userName)
        {
            var res =await _context.complaints.FirstOrDefaultAsync(x => x.UserName == userName);
            if(res == null) return null;
            return res; 
        }

       /* public async Task<bool> UpdateComplaint(string username)
        {
            var a = await _context.complaints.FirstOrDefaultAsync(x => x.UserName == username);
            if(a == null) return false;
            
            a.Description = Description;
            a.MobileNumber = .MobileNumber;
            a.ImageUrl = .ImageUrl;
           

            await _context.SaveChangesAsync();
            return true;

        }*/
    }
}
