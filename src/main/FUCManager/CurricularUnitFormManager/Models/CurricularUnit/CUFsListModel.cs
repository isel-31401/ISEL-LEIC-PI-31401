using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public sealed class CUFsListModel
    {
        private readonly List<CUFNameURIModel> _cufsListModel = new List<CUFNameURIModel>();
        private readonly String _cufType;

        public CUFsListModel(String type)
        {
            _cufType = type;
        }

        public void AddURIParams(String Name, String Action, String ACR)
        {
            _cufsListModel.Add(new CUFNameURIModel(Name, Action, ACR));
        }
        public void AddURIParams(String Name, String Action, String ACR,  ulong Version)
        {
            _cufsListModel.Add(new CUFNameURIModel(Name, Action, ACR, Version));
        }

        public IEnumerable<CUFNameURIModel> GetCUFsListModel()
        {
            return _cufsListModel.Distinct();
        }

        public String GetListModelType()
        {
            return _cufType;
        }
    }
}