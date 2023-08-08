using DataAccessLayer.Repositories;
using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelBusinessLogicLayer.BusinessComponents.BaseDefinitionsComponents
{
    public class CaseStatusComponent : IDisposable
    {
        private readonly Repository<CaseStatusModel> _repository;

        public CaseStatusComponent()
        {
            _repository = new Repository<CaseStatusModel>();
        }

        public IQueryable<CaseStatusModel> Read()
        {
            var result = _repository.SelectAllAsQuerable();
            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
