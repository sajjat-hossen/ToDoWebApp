using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface ILabelRepository : IRepository<Label>
    {
        void Update(Label label);
        void Save();
    }
}
