using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Presentation;

namespace Contracts.Factories
{
    public interface IViewModelFactory
    {
        TEntity GetViewModel <TEntity>() where TEntity : IViewModelBase;
    }
}
