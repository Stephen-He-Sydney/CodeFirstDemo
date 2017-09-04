using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using UBTTest.Data.Domain;
using UBTTest.Repository;
using UBTTest.Business.Models;
using System.Data.Entity;


namespace UBTTest.Business
{
    public class DataService : IDataService
    {
        private readonly IRepository<Make> _makeRepository;
        private readonly IRepository<Model> _modelRepository;
        public DataService(IRepository<Make> makeRepository,
            IRepository<Model> modelRepository)
        {
            _makeRepository = makeRepository;
            _modelRepository = modelRepository;
        }

        public void AddModelsBySelectedYear(Model model)
        {
            try
            {
                if (model != null)
                {
                    _modelRepository.Insert(model);
                }
                else throw new Exception("Fail to add model:" + model.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Search out all of models based on current make
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Eager loading list of models based on the selected make</returns>
        public Make SearchModelsByMakeId(int id)
        {
            try
            {
                var result = _makeRepository.Table
                                            .Include(x => x.Models)
                                            .FirstOrDefault(x => x.Id == id);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///   Search makes by fuzzy searching name
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Lazy loading list of make records</returns>
        public List<Make> SearchMakeByFuzzyName(string input)
        {
            try
            {
                var result = _makeRepository.Table
                                      .Where(x => x.Name.ToLower().Contains(input.ToLower()))
                                      .OrderBy(x=>x.Id)
                                      .Take(5) // Max only retrieve 5 
                                      .ToList();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
