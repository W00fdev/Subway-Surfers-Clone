using Subway.Logic.Data;
using System;
using System.Collections.Generic;

namespace Subway.Infrastructure.Serivces.SaveLoad
{
    public interface ISaveLoadProgressService
    {
        void SaveProgress();

        void LoadProgress();
    }
}
