using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.CurricularUnit
{
    public sealed class CUFNameURIModel
    {
        private readonly String _name;
        private readonly String _action;
        private readonly String _acr;
        private readonly ulong _version;


        public CUFNameURIModel(String Name, String Action, String ACR)
            : this(Name, Action, ACR, 0)
        {
        }
        public CUFNameURIModel(String Name, String Action, String ACR, ulong Version)
        {
            _name = Name;
            _action = Action;
            _acr = ACR;
            _version = Version;
        }

        public String GetName()
        {
            return _name;
        }

        public String GetAction()
        {
            return _action;
        }

        public String GetACR()
        {
            return _acr;
        }

        public ulong GetVersion()
        {
            return _version;
        }

    }
}