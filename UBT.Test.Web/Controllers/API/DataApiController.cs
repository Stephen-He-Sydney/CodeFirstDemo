using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UBTTest.Data.Domain;
using UBTTest.Web.Models;
using UBTTest.Business;
using System.Transactions;

namespace UBTTest.Web.Controllers.API
{
    [RoutePrefix("api/makesApi")]
    public class DataApiController : ApiController
    {
        private readonly IDataService _dataService;

        public DataApiController(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Search makes by fuzzy name
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("searchMakes/{keyword?}")]
        public HttpResponseMessage SearchMakes(string keyword="")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var result = _dataService.SearchMakeByFuzzyName(keyword);

                List<MakeViewModel> makeViewList = new List<MakeViewModel>();
                foreach (var aMake in result)
                {
                    MakeViewModel makeViewModel = new MakeViewModel()
                    {
                        Id = aMake.Id,
                        Name = aMake.Name,
                        NiceName = aMake.NiceName
                    };
                    makeViewList.Add(makeViewModel);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, makeViewList);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
                return response;
            }
            return response;
        }

        /// <summary>
        /// Search models by makeId
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("searchModelsByMakeId/{makeId}")]
        public HttpResponseMessage SearchModelsByMakeId(int makeId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var result = _dataService.SearchModelsByMakeId(makeId);

                MakeViewModel makeViewModel = new MakeViewModel();
                makeViewModel.Id = result.Id;
                makeViewModel.Name = result.Name;
                makeViewModel.NiceName = result.NiceName;

                result.Models.ToList().ForEach(x =>
                {
                    ModelViewModel modelViewModel = new ModelViewModel();
                    modelViewModel.Id = x.Id;
                    modelViewModel.Name = x.Name;
                    modelViewModel.NiceName = x.NiceName;
                    modelViewModel.Year = x.Year;
                    makeViewModel.Models.Add(modelViewModel);
                });

                response = Request.CreateResponse(HttpStatusCode.OK, makeViewModel);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
                return response;
            }
            return response;
        }

        /// <summary>
        ///  Load all makes and models data from web api to local database
        /// </summary>
        /// <param name="makes"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addMakes")]
        public HttpResponseMessage AddMakesBySelectedYear(List<MakeViewModel> makes)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    foreach (var aMake in makes)
                    {
                        Make make = new Make();
                        make.Id = aMake.Id;
                        make.Name = aMake.Name;
                        make.NiceName = aMake.NiceName;

                        foreach (var aModel in aMake.Models)
                        {
                            Model model = new Model();
                            model.Id = aModel.Id;
                            model.Name = aModel.Name;
                            model.NiceName = aModel.NiceName;
                            model.Year = aModel.Year;
                            model.Make = make;

                            _dataService.AddModelsBySelectedYear(model);
                        }
                    }
                    trans.Complete();
                }
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.ToString());
                return response;
            }
            return response;
        }
    }
}
