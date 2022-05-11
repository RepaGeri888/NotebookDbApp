using AJ4GRZ_HFT_2021221.Models;
using System.Linq;

namespace AJ4GRZ_HFT_2021221.Repository
{
    public interface INotebookRepository
    {
        void Create(Notebook notebook);
        void Delete(int id);
        Notebook Read(int id);
        IQueryable<Notebook> ReadAll();
        void Update(Notebook notebook);
    }
}