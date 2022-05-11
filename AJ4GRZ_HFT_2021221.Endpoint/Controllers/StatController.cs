using AJ4GRZ_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static AJ4GRZ_HFT_2021221.Models.StatReturnTypes;

namespace AJ4GRZ_HFT_2021221.Endpoint.Controllers
{
    [ApiController]
    public class StatController : ControllerBase
    {
        readonly INotebookLogic nl;
        readonly IGpuLogic gl;

        public StatController(INotebookLogic nl, IGpuLogic gl)
        {
            this.nl = nl;
            this.gl = gl;
        }

        [HttpGet("stat/bestgpu")]
        public IEnumerable<string> BestGpu()
        {
            return gl.BestGpu();
        }

        [HttpGet("stat/brandsavgprices")]
        public IEnumerable<BrandsAvgPrice> BrandsAvgPrices()
        {
            return nl.BrandsAvgPrices();
        }

        [HttpGet("stat/newgennotebooks")]
        public IEnumerable<KeyValuePair<string, string>> NewGenNotebooks()
        {
            return nl.NewGenNotebooks();
        }

        [HttpGet("stat/threemostpopularcpus")]
        public IEnumerable<KeyValuePair<string, int>> ThreeMostPopularCpus()
        {
            return nl.ThreeMostPopularCpus();
        }

        [HttpGet("stat/highendnotebooks")]
        public IEnumerable<HighEndNotebook> HighEndNotebooks()
        {
            return nl.HighEndNotebooks();
        }

        [HttpGet("stat/maxminprices")]
        public IEnumerable<MaxMinPrice> MaxAndMinPrices()
        {
            return nl.MostAndLeastExpensiveNotebooksByModels();
        }
    }
}
