using Passeimal.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passeimal.Backend.Dal.Repositories {
    public class StepRepository : Repository<Step>, IRepository<Step> {
        public StepRepository(IDataContext dataContext) : base(dataContext) {
            _dataContext = dataContext;
        }

        public int GetCountById(Guid id) {
            return _dataContext.Set<Step>().Count(x => x.Id == id);
        }

    } //class
}
