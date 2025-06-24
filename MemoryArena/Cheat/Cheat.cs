using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryArena
{
    public class RevealAllCheat : ICheat
    {
        public string Name => "RevealAll";

        public async Task Execute(List<Card> cards)
        {
            foreach (var c in cards.Where(c => !c.IsMatched)) c.Flip();
            await Task.Delay(5000);
            foreach (var c in cards.Where(c => !c.IsMatched)) c.HideCard();
        }
    }

    public class FindPairCheat : ICheat
    {
        public string Name => "FindPair";
        public async Task Execute(List<Card> cards)
        {
            var pair = cards
                .Where(c => !c.IsMatched && !c.IsFlipped)
                .GroupBy(c => c.ID)
                .FirstOrDefault(g => g.Count() == 2);

            if (pair != null)
            {
                foreach (var c in pair) c.Flip();
                await Task.Delay(2500);
                foreach (var c in pair) if (!c.IsMatched) c.HideCard();
            }
        }
    }

}
