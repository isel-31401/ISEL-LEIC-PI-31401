using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CurricularUnitFormManager.Models.CurricularUnit;

namespace CurricularUnitFormManager.Models.PageManagement
{
    public class Paging
    {
        public static readonly int[] itemsPerPage = { 1, 2, 5, 10 };
        public static readonly String[] orderOptions = { "Asc", "Des" };

        public static CUFsListViewModel BuildPage(int pagenr, int itemsnr, String order,
            IEnumerable<CufRepoModel> repository, String title, String method, Boolean hasVersion)
        {
            int amountPages = ((repository.Count() + itemsnr - 1) / itemsnr);
            CUFsListModel clm = new CUFsListModel(title);

            repository = (order == "Asc") ? repository.OrderBy(cuf => cuf.Name) : repository.OrderByDescending(cuf => cuf.Name);

            foreach (var cuf in repository.Skip(pagenr * itemsnr).Take(itemsnr))
            {
                if (hasVersion)
                    clm.AddURIParams(cuf.Name + " [ V: " + cuf.Version + " ]", method, cuf.Acr, cuf.Version); //proposal-version
                else
                    clm.AddURIParams(cuf.Name, method, cuf.Acr);
                
            }

            CUFsListViewModel clvm = new CUFsListViewModel();
            clvm.CUFList = clm.GetCUFsListModel();
            clvm.PageNumber = pagenr;
            clvm.ItemsNumber = itemsnr;
            clvm.Order = order;
            clvm.AmountPage = amountPages;
            clvm.LastPage = false;
            clvm.ItemsPerPage = itemsPerPage;
            clvm.OrderPage = orderOptions;

            clvm.Method = hasVersion ? "PageVersion" : "Page";

            if (clvm.CUFList.Count() < itemsnr ||
                repository.Skip((pagenr + 1) * itemsnr).Take(itemsnr).Count() == 0)
                clvm.LastPage = true;

            return clvm;
        }

        public static bool IsAjaxRequest(HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return (request["X-Requested-With"] == "XMLHttpRequest") || 
                ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        }

    }
}