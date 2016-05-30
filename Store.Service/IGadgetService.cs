using Store.Data.Infrastructure;
using Store.Data.Repositories;
using Store.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service
{
    public interface IGadgetService
    {
        IEnumerable<Gadget> GetGudgets();
        IEnumerable<Gadget> GetCategoryGadgets(string categoryName, string gadgetName = null);
        Gadget GetGadget(int id);
        void CreateGadget(Gadget gadget);
        void SaveGadget();
    }
    public class GadgetService:IGadgetService
    {
        private readonly IGadgetRepository gadgetRepository;
        private readonly ICategoryRepository categoryReposory;
        private readonly IUnitOfWork unitOfWork;

        public GadgetService(IGadgetRepository gadgetRepository, ICategoryRepository, ICategoryRepository categoryReposory, IUnitOfWork unitOfWork)
        {
            this.gadgetRepository = gadgetRepository;
            this.categoryReposory = categoryReposory;
            this.unitOfWork = unitOfWork;
        }
        #region IGadgetServiceMembers
        public IEnumerable<Gadget> GetGudgets()
        {
            var gadgets = gadgetRepository.GetAll();
            return gadgets;
        }

        public IEnumerable<Gadget> GetCategoryGadgets(string categoryName,string gadgetName = null)
        {
            var category = categoryReposory.GetCategoryByName(categoryName);
            return category.Gadgets.Where(g => g.Name.ToLower().Contains(gadgetName.ToLower().Trim()));
        }

        public Gadget GetGadget(int id)
        {
            var gadget = gadgetRepository.GetById(id);
            return gadget;
        }

        public void CreateGadget(Gadget gadget)
        {
            gadgetRepository.Add(gadget);
        }

        public void SaveGadget()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
