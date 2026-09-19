using System.Linq.Expressions;
using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.DTOs;
using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class LabTestServices : ILabTestServices
    {
        private readonly MLMSDbContext context;

        public LabTestServices(MLMSDbContext context)
        {
            this.context = context;
        }

        public int Add(AddLabTestDTO labTestDTO)
        {
            var newLabTest = new LabTest()
            {
                Name = labTestDTO.Name,
                Price = labTestDTO.Price
            };
            context.Add(newLabTest);
            context.SaveChanges();
            return newLabTest.Id;
        }

        public int? Delete(int id)
        {
            var labTest = context.LabTests
                .Where(x => x.Id == id).FirstOrDefault();
            if (labTest == null)
                return null;
            labTest.IsDeleted = true;
            context.SaveChanges();
            return labTest.Id;

        }

        public int? Edit(int id, EditLabTestDTO labTestDTO)
        {
            var labTest = context.LabTests
                .Where(x => x.Id == id).FirstOrDefault();
            if (labTest == null)
                return null;
            labTest.Name = labTestDTO.Name ?? labTest.Name;
            labTest.Price = labTestDTO.Price ?? labTest.Price;
            context.SaveChanges();
            return labTest.Id;
        }

        public List<LabTestDetailsDTO> GetAll()
        {
            var labTests = context.LabTests
                .Select(ToLabTestDetailsDTO())
                .ToList();
            return labTests;
        }

        public List<LabTest> GetAllWithIds(List<int> ids)
        {
            return context.LabTests.Where(x => ids.Contains(x.Id)).ToList();
        }

        public LabTestDetailsDTO? GetById(int id)
        {
            return context.LabTests
                .Where(x => x.Id == id)
                .Select(ToLabTestDetailsDTO())
                .FirstOrDefault();
        }

        private Expression<Func<LabTest, LabTestDetailsDTO>> ToLabTestDetailsDTO()
        {
            return x => new LabTestDetailsDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            };
        } 
    }
}
