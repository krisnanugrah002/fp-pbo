using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryArena
{
    public interface ICheat
    {
        string Name { get; }
        Task Execute(List<Card> cards);
    }

}
