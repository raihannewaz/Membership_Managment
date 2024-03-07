using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Membership_Managment.DAL.Repositories
{
    public class MemberRepository : IMemberRepository
    {

        private ApplicationDbContext _context;
        private IWebHostEnvironment _hostEnvironment;



        public MemberRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Member> Add(Member entity)
        {

            if (entity.ImageFile != null)
            {
                if (!IsImageFileValid(entity.ImageFile.FileName))
                {
                    throw new ArgumentException("Invalid image file format. Please upload a JPG or PNG file.");
                }
                entity.Photo = await UploadImageAsync(entity.ImageFile);
            }


            entity.CreateDate = DateTime.Now;
            entity.ActivaitonDate = DateTime.Now;
            entity.ExpDate = DateTime.Now.AddMinutes(1);
            entity.IsActive = true;
            entity.ActionType = "Create";
            entity.ActionDate= DateTime.Now;
            await _context.Members.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<Member> Delete(Member entity)
        {
            _context.Members.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<Member>> GetAllAsync()
        {

            return await _context.Members.Include(m => m.DocumentList).Include(a=>a.FeeCollection).ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await _context.Members.Include(m => m.DocumentList).Include(a=>a.FeeCollection).FirstOrDefaultAsync(o => o.MemberId == id);
        }


        public async Task<Member> Update(int id, Member entity)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new ArgumentException("Entity with the given id not found.");
            }

            
            if (entity.IsActive == true)
            {
                existingEntity.ActivaitonDate = DateTime.Now;
                existingEntity.ExpDate = DateTime.Now.AddMinutes(1);
            }

           
            if (entity.ImageFile != null)
            {
            
                if (!string.IsNullOrEmpty(existingEntity.Photo))
                {
                    string existingPhotoPath = existingEntity.Photo;
                    if (File.Exists(existingPhotoPath))
                    {
                        File.Delete(existingPhotoPath);
                    }
                }
                
                existingEntity.Photo = await UploadImageAsync(entity.ImageFile);
            }

            entity.ActionType = "Update";
            entity.ActionDate = DateTime.Now;

            foreach (var property in typeof(Member).GetProperties())
            {
                var newValue = property.GetValue(entity);
                if (newValue != null)
                {
                    property.SetValue(existingEntity, newValue);
                }
            }


            await _context.SaveChangesAsync();

            return existingEntity;
        }


        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "MemberImage\\");
            string uniqueFileName = DateTime.Now.Ticks.ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            
            var imgUrl = uploadsFolder + uniqueFileName;


            return imgUrl;
        }



        private bool IsImageFileValid(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }


        public async Task UpdateExpiredMembersStatus()
        {
            var expiredMembers = await _context.Members
                .Where(m => m.ExpDate <= DateTime.Now && (m.IsActive ?? false))
                .ToListAsync();

            foreach (var member in expiredMembers)
            {
                member.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }


    }
}
