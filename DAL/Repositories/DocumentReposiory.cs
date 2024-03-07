using Membership_Managment.Context;
using Membership_Managment.DAL.Interfaces;
using Membership_Managment.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Xml.Linq;

namespace Membership_Managment.DAL.Repositories
{
    public class DocumentReposiory : IDocumentRepository
    {
        private ApplicationDbContext _context;
        private IWebHostEnvironment _hostEnvironment;



        public DocumentReposiory(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Document> AddNew(int memberId, Document document)
        {


            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
            {
                throw new ArgumentNullException("Member not found");
            }

            var nidDocument = await CreateNidDocument(memberId, Document.EDocType.Nid, document.NidFile);
            var utilityDocument = await CreateUtilityDocument(memberId, Document.EDocType.UtilityBill, document.UtilityFile);

            _context.Documents.AddRange(nidDocument, utilityDocument);
            await _context.SaveChangesAsync();
            return nidDocument;
        }



        public async Task<Document> UpdateDoc(int memberId, Document document)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null)
            {
                throw new ArgumentNullException("Member not found");
            }


            var nidDocument = await UpdateOrCreateNidDocument(memberId, Document.EDocType.Nid, document.NidFile);
            var utilityDocument = await UpdateOrCreateUtilityDocument(memberId, Document.EDocType.UtilityBill, document.UtilityFile);


            await _context.SaveChangesAsync();

            return nidDocument ?? utilityDocument;
        }


        private async Task<Document> UpdateOrCreateUtilityDocument(int memberId, Document.EDocType documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (!IsImageFileValid(file.FileName))
            {
                throw new ArgumentException("Invalid image file format. Please upload a JPG or PNG file.");
            }

            var existingDocument = await _context.Documents.FirstOrDefaultAsync(d =>
                d.MemberId == memberId && d.DocumentType == documentType.ToString());

            if (existingDocument != null)
            {
                if (!string.IsNullOrEmpty(existingDocument.DocumentLocation))
                {
                    string existingPhotoPath = existingDocument.DocumentLocation;
                    if (File.Exists(existingPhotoPath))
                    {
                        File.Delete(existingPhotoPath);
                    }
                }
                existingDocument.DocumentLocation = await UploadDocImgAsync(file, documentType.ToString());
                existingDocument.ActionType = "Update";
                existingDocument.ActionDate = DateTime.Now;
            }
            else
            {
                var documentLocation = await UploadDocImgAsync(file, documentType.ToString());
                var newDocument = new Document
                {
                    DocumentType = documentType.ToString(),
                    DocumentLocation = documentLocation,
                    MemberId = memberId
                };
                _context.Documents.Add(newDocument);
            }

            return existingDocument;
        }


        private async Task<Document> UpdateOrCreateNidDocument(int memberId, Document.EDocType documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (!IsDocFileValid(file.FileName))
            {
                throw new ArgumentException("Invalid file format. Please upload a pdf file.");
            }

            var existingDocument = await _context.Documents.FirstOrDefaultAsync(d =>
                d.MemberId == memberId && d.DocumentType == documentType.ToString());

            if (existingDocument != null)
            {
                if (!string.IsNullOrEmpty(existingDocument.DocumentLocation))
                {
                    string existingPhotoPath = existingDocument.DocumentLocation;
                    if (File.Exists(existingPhotoPath))
                    {
                        File.Delete(existingPhotoPath);
                    }
                }
                existingDocument.DocumentLocation = await UploadDocImgAsync(file, documentType.ToString());
                existingDocument.ActionType = "Update";
                existingDocument.ActionDate = DateTime.Now;
            }
            else
            {
                var documentLocation = await UploadDocImgAsync(file, documentType.ToString());
                var newDocument = new Document
                {
                    DocumentType = documentType.ToString(),
                    DocumentLocation = documentLocation,
                    MemberId = memberId
                };
                _context.Documents.Add(newDocument);
            }

            return existingDocument;
        }



        private async Task<Document> CreateUtilityDocument(int memberId, Document.EDocType documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw null;
            }

            if (!IsImageFileValid(file.FileName))
            {
                throw new ArgumentException("Invalid image file format. Please upload a JPG or PNG file.");
            }

            var documentLocation = await UploadDocImgAsync(file, documentType.ToString());
            var document = new Document
            {
                DocumentType = documentType.ToString(),
                DocumentLocation = documentLocation,
                MemberId = memberId,
                ActionType = "Create",
                ActionDate = DateTime.Now
            };

            return document;
        }



        private async Task<Document> CreateNidDocument(int memberId, Document.EDocType documentType, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw null;
            }

            if (!IsDocFileValid(file.FileName))
            {
                throw new ArgumentException("Invalid file format. Please upload a pdf file.");
            }

            var documentLocation = await UploadDocImgAsync(file, documentType.ToString());
            var document = new Document
            {
                DocumentType = documentType.ToString(),
                DocumentLocation = documentLocation,
                MemberId = memberId,
                ActionType = "Create",
                ActionDate = DateTime.Now
            };

            return document;
        }

        private async Task<string> UploadDocImgAsync(IFormFile docFile, string type)
        {
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "DocumentImage\\");
            string uniqueFileName = DateTime.Now.Ticks.ToString() + "_" + type + "_" + docFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await docFile.CopyToAsync(fileStream);
            }
            var imgUrl = uploadsFolder + uniqueFileName;


            return imgUrl;
        }


        private bool IsImageFileValid(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".jpeg" || extension == ".png";
        }

        private bool IsDocFileValid(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".pdf";
        }



        public string GetNidByMember(int memberId)
        {
            var type = "Nid";
            var documentLocation = _context.Documents
                .Where(d => d.MemberId == memberId && d.DocumentType == type)
                .Select(d => d.DocumentLocation).FirstOrDefault();

            return documentLocation;

        }

        public string GetUtilityByMember(int memberId)
        {
            var type = "UtilityBill";
            var documentLocation = _context.Documents
                .Where(d => d.MemberId == memberId && d.DocumentType == type)
                .Select(d => d.DocumentLocation).FirstOrDefault();

            return documentLocation;
        }




        public Task<Document> Delete(Document entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Document>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Document> Add(Document entity)
        {
            throw new NotImplementedException();
        }


        public async Task<Document> Update(int id, Document entity)
        {
            throw new NotImplementedException();
        }
    }
}
