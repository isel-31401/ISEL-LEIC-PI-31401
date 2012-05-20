using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurricularUnitFormManager.Models.Individual
{
    public class IndividualRepoModel
    {
        string _individualUserName;
        string _individualFullName;
        int _individualID;

        public IndividualRepoModel(Individual user)
        {
            _individualUserName = user.UserName;
            _individualFullName = user.FullName;
            _individualID = user.IndividualId;
        }
        public String UserName { get { return _individualUserName; } }
        public String FullName { get { return _individualFullName; } }
        public int ID { get { return _individualID; }}
    }
}